using Scripts.SwitchWindowBase;
using Scripts.UI;
using Scripts.WindowBase.Breed;
using Scripts.WindowBase.Breed.PopUp;
using Scripts.WindowBase.Breed.Window;
using Scripts.WindowBase.Weather;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private UICanvas _canvasReference;
        [SerializeField] private SwitchWindow _switchWindowPrefab;
        [SerializeField] private WindowWeather _windowWeatherPrefab;
        [SerializeField] private WindowBreeds _windowBreedsPrefab;
        [SerializeField] private PopUpBreed _popUpBreedPrefab;
        [SerializeField] private BreedItem _breedItemPrefab;
        [SerializeField] private QueueService _queueServicePrefab;
        [SerializeField] private ShowProgress _showProgressPrefab;
        
        public override void InstallBindings()
        {
            Container
                .Bind<UICanvas>()
                .FromInstance(_canvasReference)
                .AsSingle();
            
            BindWindows(); 
            
            BindPools();
            
            Container
                .Bind<QueueService>()
                .FromComponentInNewPrefab(_queueServicePrefab)
                .AsSingle().
                NonLazy();
        }
    
        private void BindWindows()
        {
            Container.
                BindFactory<SwitchWindow, SwitchWindow.SwitchWindowFactory>().
                FromComponentInNewPrefab(_switchWindowPrefab);
            
            Container.
                BindMemoryPool<WindowWeather, WindowWeatherPool>().
                FromComponentInNewPrefab(_windowWeatherPrefab);
            
            Container.
                BindMemoryPool<WindowBreeds, WindowBreedsPool>().
                FromComponentInNewPrefab(_windowBreedsPrefab);
        }
    
        private void BindPools()
        {
            Container.
                BindMemoryPool<PopUpBreed, PopUpBreedPool>().
                FromComponentInNewPrefab(_popUpBreedPrefab);
            
            Container.
                BindMemoryPool<BreedItem, BreedItemPool>().
                FromComponentInNewPrefab(_breedItemPrefab);
            
            Container.
                BindMemoryPool<ShowProgress, ShowProgressPool>().
                FromComponentInNewPrefab(_showProgressPrefab);
        }
    }
}
