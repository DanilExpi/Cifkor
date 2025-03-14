using System.Collections;
using UnityEngine;
using Zenject;

namespace Scripts.UI
{
    public class ShowProgress : MonoBehaviour
    {
        [SerializeField] private float speedRotate = 60f;
        [SerializeField] private Transform _centerRotate;

        private Coroutine _rotateCor;
        private UICanvas _uiCanvas;

        [Inject]
        private void Construct(UICanvas uiCanvas)
        {
            _uiCanvas = uiCanvas;
        }

        private void Start()
        {
            transform.SetParent(_uiCanvas.GetRootShowProgress, false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            if (_rotateCor != null)
            {
                StopCoroutine(_rotateCor);
            }

            _rotateCor = StartCoroutine(RotateCor());
        }

        public void Hide()
        {
            if (_rotateCor != null)
            {
                StopCoroutine(_rotateCor);
                _rotateCor = null;
            }

            gameObject.SetActive(false);
        }

        private IEnumerator RotateCor()
        {
            while (true)
            {
                _centerRotate.Rotate(Vector3.back, Time.deltaTime * speedRotate);
                yield return null;
            }
        }
    }

    public class ShowProgressPool : MemoryPool<ShowProgress>
    {
        protected override void OnSpawned(ShowProgress item)
        {
            item.Show();
        }

        protected override void OnDespawned(ShowProgress item)
        {
            item.Hide();
        }
    }
}