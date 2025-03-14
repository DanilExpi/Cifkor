using UnityEngine;
using Zenject;

namespace Scripts.WindowBase
{
    public abstract class ModelWindow : MonoBehaviour, IQueueWaiter
    {
        [SerializeField] private Window _window;

        private QueueService _queueService;

        [Inject]
        private void Construct(QueueService queueService)
        {
            _queueService = queueService;
        }

        public void SetResult(IQueueResult result)
        {
            _window.SetResult(result);
        }

        protected void WaitResult(IQueueItem queueItem)
        {
            _queueService.Wait(queueItem);
        }

        public void StopWait()
        {
            _queueService.StopWait(this);
        }
    }
}