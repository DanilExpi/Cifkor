using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.WindowBase.Weather
{
    public class ViewWindowWeather : ViewWindow
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private RawImage _image;

        public void DropVisual()
        {
            _image.texture = null;
            _image.enabled = false;
            _title.text = string.Empty;
        }

        public void SetResult(IQueueResult result)
        {
            var weatherResult = result as QueueResultWeather;
            if (weatherResult == null) return;

            _image.texture = weatherResult.icon;
            _image.enabled = true;
            _title.text = $"{weatherResult.temperatureUnit}{weatherResult.temperature}";
        }
    }
}