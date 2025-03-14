using Scripts.WindowBase.Breed.Window;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.WindowBase.Breed
{
    public class BreedItem : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI title;

        public string _id;
        private WindowBreeds _windowBreeds;

        private void Start()
        {
            _button.onClick.AddListener(OnPress);
        }

        private void OnPress()
        {
            _windowBreeds.ShowPopUp(_id);
        }

        public void Init(WindowBreeds windowBreeds, string id, string nameBreed, RectTransform root, int siblingIndex)
        {
            _windowBreeds = windowBreeds;
            transform.SetParent(root);
            transform.SetSiblingIndex(siblingIndex);
            _id = id;
            title.text = nameBreed;
        }
    }

    public class BreedItemPool : MemoryPool<BreedItem>
    {
        protected override void OnSpawned(BreedItem item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(BreedItem item)
        {
            item.gameObject.SetActive(false);
        }
    }
}
