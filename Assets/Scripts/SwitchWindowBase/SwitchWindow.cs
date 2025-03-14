using Scripts.WindowBase;
using Scripts.WindowBase.Breed.Window;
using Scripts.WindowBase.Weather;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.SwitchWindowBase
{
    public class SwitchWindow : MonoBehaviour
    {
        public class SwitchWindowFactory : PlaceholderFactory<SwitchWindow>
        {
        }

        [SerializeField] private Button _buttonWeather, _buttonBreeds;
        [SerializeField] private ViewSwitchWindow _view;

        private WindowWeatherPool _windowWeatherPool;
        private WindowBreedsPool _windowBreedsPool;

        private Window _currentWindow;
        private WindowPool _currentWindowPool;

        [Inject]
        private void Construct(WindowWeatherPool windowWeatherPool, WindowBreedsPool windowBreedsPool)
        {
            _windowWeatherPool = windowWeatherPool;
            _windowBreedsPool = windowBreedsPool;
        }

        public void Init()
        {
            _buttonWeather.onClick.AddListener(OnClickWeatherButton);
            _buttonBreeds.onClick.AddListener(OnClickBreedsButton);
            _view.Init();
            OnClickWeatherButton();
        }

        private void OnClickWeatherButton()
        {
            _view.InteractableButton(_buttonBreeds);
            CreateWindow(_windowWeatherPool);
        }

        private void OnClickBreedsButton()
        {
            _view.InteractableButton(_buttonWeather);
            CreateWindow(_windowBreedsPool);
        }

        private void CreateWindow(WindowPool pool)
        {
            if (_currentWindowPool == pool)
            {
                return;
            }

            if (_currentWindow != null)
            {
                _currentWindowPool.Despawn(_currentWindow);
            }

            _currentWindowPool = pool;
            _currentWindow = pool.Spawn();
        }
    }
}
