using Scripts.UI;
using UnityEngine;
using Zenject;

namespace Scripts.WindowBase
{
    public abstract class ViewWindow : MonoBehaviour
    {
        private ShowProgressPool _showProgressPool;
        private ShowProgress _showProgress;

        private UICanvas _uiCanvas;

        [Inject]
        private void Construct(UICanvas uiCanvas, ShowProgressPool showProgressPool)
        {
            _uiCanvas = uiCanvas;
            _showProgressPool = showProgressPool;
        }

        private void Start()
        {
            transform.SetParent(_uiCanvas.GetRoot, false);
        }

        public void ShowProgress()
        {
            if (_showProgress == null)
            {
                _showProgress = _showProgressPool.Spawn();
            }

            _showProgress.Show();
        }

        public void HideProgress()
        {
            if (_showProgress != null)
            {
                _showProgressPool.Despawn(_showProgress);
                _showProgress = null;
            }
        }
    }
}
