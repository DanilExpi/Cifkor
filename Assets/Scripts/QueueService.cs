using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts
{
    public class QueueService : MonoBehaviour
    {
        private List<IQueueItem> _items = new List<IQueueItem>();
        private Coroutine _waitCor;

        private bool IsWait => _waitCor != null;

        public void Wait(IQueueItem item)
        {
            _items.Add(item);
            if (!IsWait)
            {
                _waitCor = StartCoroutine(WaitCor());
            }
        }

        public void StopWait(IQueueWaiter waiter)
        {
            if (_items.Count == 0) return;

            if (_items.First().Waiter == waiter && IsWait)
            {
                StopCoroutine(_waitCor);
                _waitCor = null;
            }

            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                if (item.Waiter != waiter) continue;

                _items.RemoveAt(i);
                i--;
            }

            if (_items.Any() && !IsWait)
            {
                _waitCor = StartCoroutine(WaitCor());
            }
        }

        private IEnumerator WaitCor()
        {
            while (_items.Any())
            {
                var firstItem = _items.First();
                yield return firstItem.WaitLoad();
                var waiter = firstItem.Waiter;
                var result = firstItem.GetResult();
                waiter.SetResult(result);
                _items.RemoveAt(0);
            }

            _waitCor = null;
        }
    }

    public interface IQueueItem
    {
        IQueueWaiter Waiter { get; set; }
        IEnumerator WaitLoad();
        IQueueResult GetResult();
    }

    public interface IQueueWaiter
    {
        void SetResult(IQueueResult result);
    }

    public interface IQueueResult
    {

    }
}