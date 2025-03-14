using UnityEngine;
using Zenject;

namespace Scripts.WindowBase.Breed.Window
{
    public class ViewWindowBreeds : ViewWindow
    {
        [SerializeField] private RectTransform _rootItems;
        [SerializeField] private WindowBreeds windowBreeds;
        private BreedItem[] _items;

        private BreedItemPool _BreedsItemPool;

        [Inject]
        private void Construct(BreedItemPool breedItemPool)
        {
            _BreedsItemPool = breedItemPool;
        }

        public void HideItems()
        {
            if (_items == null) return;
            for (int i = 0; i < _items.Length; i++)
            {
                _BreedsItemPool.Despawn(_items[i]);
            }

            _items = null;
        }

        public void ShowItems(IQueueResult result)
        {
            var breedsResult = result as QueueResultBreeds;
            if (breedsResult == null) return;

            var breeds = breedsResult.breeds;
            _items = new BreedItem[breeds.Length];
            for (int i = 0; i < breeds.Length; i++)
            {
                var breed = breeds[i];
                var item = _BreedsItemPool.Spawn();
                item.Init(windowBreeds, breed.id, breed.name, _rootItems, i);
                _items[i] = item;
            }
        }
    }
}
