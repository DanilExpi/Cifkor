using UnityEngine;
using Zenject;

namespace Scripts.WindowBase
{
    public abstract class Window : MonoBehaviour
    {
        private ModelWindow _modelWindow;

        private void Awake()
        {
            _modelWindow = GetComponent<ModelWindow>();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            HideProgress();
            OnShow();
        }

        protected abstract void OnShow();

        public void Hide()
        {
            gameObject.SetActive(false);
            OnHide();

            StopQueue();
            HideProgress();
        }

        protected abstract void OnHide();

        public abstract void SetResult(IQueueResult result);

        private void StopQueue()
        {
            _modelWindow.StopWait();
        }

        protected abstract void ShowProgress();
        protected abstract void HideProgress();
    }

    public class WindowPool : MemoryPool<Window>
    {
        protected override void OnSpawned(Window item)
        {
            item.Show();
        }

        protected override void OnDespawned(Window item)
        {
            item.Hide();
        }
    }
}
