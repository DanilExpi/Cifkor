using Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.SwitchWindowBase
{
    public class ViewSwitchWindow : MonoBehaviour
    {
        [SerializeField] private Button[] _buttons;

        private UICanvas _uiCanvas;

        [Inject]
        private void Construct(UICanvas uiCanvas)
        {
            _uiCanvas = uiCanvas;
        }

        public void Init()
        {
            transform.SetParent(_uiCanvas.GetRoot, false);
        }

        public void InteractableButton(Button interactableButton)
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].interactable = interactableButton == _buttons[i];
            }
        }
    }
}