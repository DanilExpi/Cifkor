using System.Collections;
using UnityEngine;

namespace Scripts.WindowBase.Weather
{
    public class WindowWeather : Window
    {
        [SerializeField] private ModelWindowWeather _model;
        [SerializeField] private ViewWindowWeather _view;
        [SerializeField] private float _timerValue = 5f;

        private Coroutine _waitCor;

        protected override void OnShow()
        {
            ShowProgress();
            _waitCor = StartCoroutine(WaitTimerCor());

            _view.DropVisual();
        }

        protected override void OnHide()
        {
            StopTimer();
        }

        public override void SetResult(IQueueResult result)
        {
            HideProgress();
            _view.SetResult(result);
        }

        protected override void ShowProgress()
        {
            _view.ShowProgress();
        }

        protected override void HideProgress()
        {
            _view.HideProgress();
        }

        private void StopTimer()
        {
            if (_waitCor != null)
            {
                StopCoroutine(_waitCor);
                _waitCor = null;
            }
        }

        private IEnumerator WaitTimerCor()
        {
            while (true)
            {
                yield return new WaitForSeconds(_timerValue);
                _model.FindWeather();
            }
        }
    }

    public class WindowWeatherPool : WindowPool
    {
    }
}