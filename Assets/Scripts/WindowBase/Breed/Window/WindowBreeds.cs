using Scripts.WindowBase.Breed.PopUp;
using UnityEngine;
using Zenject;

namespace Scripts.WindowBase.Breed.Window
{
    public class WindowBreeds : WindowBase.Window
    {
        [SerializeField] private ModelWindowBreeds _model;
        [SerializeField] private ViewWindowBreeds _view;
        [SerializeField] private RectTransform _rootPopUp;

        private PopUpBreedPool _popUpBreedPool;
        private PopUpBreed _popUp;

        private bool IsShowPopUp => _popUp && _popUp.IsShow;

        [Inject]
        private void Construct(PopUpBreedPool popUpBreedPool)
        {
            _popUpBreedPool = popUpBreedPool;
        }

        protected override void OnShow()
        {
            _view.HideItems();
            HidePopUp();
            ShowProgress();

            _model.FindBreeds();
        }

        protected override void OnHide()
        {
            _view.HideItems();
            HidePopUp();
        }

        private void HidePopUp()
        {
            if (IsShowPopUp)
            {
                _popUpBreedPool.Despawn(_popUp);
                _popUp = null;
            }
        }

        public void ShowPopUp(string id)
        {
            if (IsShowPopUp)
            {
                if (_popUp.GetId == id) return;
            }

            HidePopUp();
            _model.FindBreed(id);
            ShowProgress();
        }

        public override void SetResult(IQueueResult result)
        {
            HideProgress();

            var breedsResult = result as QueueResultBreeds;
            if (breedsResult != null)
            {
                _view.ShowItems(breedsResult);
                return;
            }

            var breedResult = result as QueueResultBreedPopUp;
            if (breedResult != null)
            {
                if (IsShowPopUp)
                {
                    if (_popUp.GetId == breedResult.id) return;
                }

                if (!IsShowPopUp)
                {
                    _popUp = _popUpBreedPool.Spawn();
                }

                _popUp.Init(_rootPopUp, breedResult.name, breedResult.description);
            }
        }

        protected override void ShowProgress()
        {
            _view.ShowProgress();
        }

        protected override void HideProgress()
        {
            _view.HideProgress();
        }
    }

    public class WindowBreedsPool : WindowPool
    {
    }
}
