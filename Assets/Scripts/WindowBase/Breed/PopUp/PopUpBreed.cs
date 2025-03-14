using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.WindowBase.Breed.PopUp
{
    public class PopUpBreed : MonoBehaviour
    {
        [SerializeField] private ViewPopUpBreed _view;
        [SerializeField] private Button _closeButton;
        [SerializeField] private float _upOffset, _bottomOffset;

        private string _id;
        private PopUpBreedPool _popUpBreedPool;
        private PopUpBreed _popUp;

        public string GetId => _id;
        public bool IsShow => gameObject.activeInHierarchy;

        [Inject]
        private void Construct(PopUpBreedPool popUpBreedPool)
        {
            _popUpBreedPool = popUpBreedPool;
        }

        private void Start()
        {
            _closeButton.onClick.AddListener(OnPressCloseButton);
        }

        private void OnPressCloseButton()
        {
            _popUpBreedPool.Despawn(this);
        }

        public void Init(RectTransform parent, string title, string description)
        {
            _view.Init(parent, title, description);
        }

        public void Hide()
        {
            _id = string.Empty;
            _view.DropVisual();
            gameObject.SetActive(false);
        }
    }

    public class PopUpBreedPool : MemoryPool<PopUpBreed>
    {
        protected override void OnSpawned(PopUpBreed item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(PopUpBreed item)
        {
            item.Hide();
        }
    }
}
