using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Scripts.WindowBase.Weather
{
    public class ModelWindowWeather : ModelWindow
    {
        public void FindWeather()
        {
            WaitResult(new QueueItemWeather()
            {
                Waiter = this
            });
        }
    }

    [Serializable]
    public struct Forecast
    {
        public Properties properties;
    }

    [Serializable]
    public struct Properties
    {
        public Period[] periods;
    }

    [Serializable]
    public struct Period
    {
        public string temperature;
        public string temperatureUnit;
        public string icon;
    }

    public class QueueItemWeather : IQueueItem
    {
        private QueueResultWeather Result;

        private const string _url = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";

        public IQueueWaiter Waiter { get; set; }

        public IEnumerator WaitLoad()
        {
            UnityWebRequest requestForecast = UnityWebRequest.Get(_url);

            yield return requestForecast.SendWebRequest();

            var json = requestForecast.downloadHandler.text;

            Forecast forecast = JsonUtility.FromJson<Forecast>(json);

            var urlIcon = forecast.properties.periods[0].icon;
            Texture texture = null;
            UnityWebRequest requestTexture = UnityWebRequestTexture.GetTexture(urlIcon);
            yield return requestTexture.SendWebRequest();

            if (requestTexture.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(requestTexture.error);
            }
            else
            {
                texture = DownloadHandlerTexture.GetContent(requestTexture);
            }

            Result = new QueueResultWeather()
            {
                temperatureUnit = forecast.properties.periods[0].temperatureUnit,
                temperature = forecast.properties.periods[0].temperature,
                icon = texture
            };
        }

        public IQueueResult GetResult()
        {
            return Result;
        }
    }

    public class QueueResultWeather : IQueueResult
    {
        public string temperatureUnit;
        public string temperature;
        public Texture icon;
    }
}