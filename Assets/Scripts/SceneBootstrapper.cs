using Scripts.SwitchWindowBase;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class SceneBootstrapper : MonoBehaviour
    {
        private SwitchWindow.SwitchWindowFactory _switchWindowFactory;

        [Inject]
        private void Construct(SwitchWindow.SwitchWindowFactory switchWindowFactory)
        {
            _switchWindowFactory = switchWindowFactory;
        }

        private void Start()
        {
            CreateSwitchWindow();
        }

        private void CreateSwitchWindow()
        {
            var switchWindow = _switchWindowFactory.Create();
            switchWindow.Init();
        }
    }
}