using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Scripts.WindowBase.Breed.Window
{
    public class ModelWindowBreeds : ModelWindow
    {
        public void FindBreeds()
        {
            WaitResult(new QueueItemBreeds()
            {
                Waiter = this,
            });
        }

        public void FindBreed(string id)
        {
            WaitResult(new QueueItemBreed()
            {
                id = id,
                Waiter = this,
            });
        }
    }

    [Serializable]
    public struct Breeds
    {
        public BreedData[] data;
    }

    [Serializable]
    public struct BreedData
    {
        public string id;
        public BreedAttributes attributes;
    }

    [Serializable]
    public struct BreedAttributes
    {
        public string name;
        public string description;
    }

    public class QueueItemBreeds : IQueueItem
    {
        private QueueResultBreeds Result;

        private const string _url = "https://dogapi.dog/api/v2/breeds";

        public IQueueWaiter Waiter { get; set; }

        public IEnumerator WaitLoad()
        {
            UnityWebRequest requestBreeds = UnityWebRequest.Get(_url);
            yield return requestBreeds.SendWebRequest();

            var json = requestBreeds.downloadHandler.text;

            Breeds breeds = JsonUtility.FromJson<Breeds>(json);

            Result = new QueueResultBreeds()
            {
                breeds = new QueueResultBreed[breeds.data.Length]
            };

            for (int i = 0; i < breeds.data.Length; i++)
            {
                Result.breeds[i] = new QueueResultBreed()
                {
                    id = breeds.data[i].id,
                    name = breeds.data[i].attributes.name
                };
            }
        }

        public IQueueResult GetResult()
        {
            return Result;
        }
    }

    public class QueueResultBreeds : IQueueResult
    {
        public QueueResultBreed[] breeds;
    }

    public class QueueResultBreed
    {
        public string id;
        public string name;
    }

    public class QueueItemBreed : IQueueItem
    {
        private QueueResultBreedPopUp Result;

        private string _url = "https://dogapi.dog/api/v2/breeds/";

        public string id;

        public IQueueWaiter Waiter { get; set; }

        public IEnumerator WaitLoad()
        {
            var urlBreed = $"{_url}{id}";
            UnityWebRequest requestBreed = UnityWebRequest.Get(urlBreed);

            yield return requestBreed.SendWebRequest();

            var json = requestBreed.downloadHandler.text;

            SingleBreed singleBreed = JsonUtility.FromJson<SingleBreed>(json);

            Result = new QueueResultBreedPopUp()
            {
                id = singleBreed.data.id,
                name = singleBreed.data.attributes.name,
                description = singleBreed.data.attributes.description,
            };
        }

        public IQueueResult GetResult()
        {
            return Result;
        }
    }

    [Serializable]
    public struct SingleBreed
    {
        public BreedData data;
    }

    public class QueueResultBreedPopUp : IQueueResult
    {
        public string id;
        public string name;
        public string description;
    }
}
