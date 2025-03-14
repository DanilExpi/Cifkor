using UnityEngine;

namespace Scripts.UI
{
    public class UICanvas : MonoBehaviour
    {
        [SerializeField] private RectTransform _root, _rootShowProgress;

        public RectTransform GetRoot => _root;
        public RectTransform GetRootShowProgress => _rootShowProgress;
    }
}