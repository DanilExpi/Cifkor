using TMPro;
using UnityEngine;

namespace Scripts.WindowBase.Breed.PopUp
{
    public class ViewPopUpBreed : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleName, _description;
        [SerializeField] private RectTransform _body, _closeButtonRect;
        [SerializeField] private float _upOffset, _bottomOffset;

        public void DropVisual()
        {
            _titleName.text = string.Empty;
            _description.text = string.Empty;
        }

        public void Init(RectTransform parent, string title, string description)
        {
            transform.SetParent(parent, false);
            _titleName.text = title;
            _description.text = description;

            ResetBody();
        }

        private void ResetBody()
        {
            var preferredHeightDescription = _description.preferredHeight;
            _description.rectTransform.sizeDelta =
                new Vector2(_description.rectTransform.sizeDelta.x, preferredHeightDescription);

            var preferredHeightTitle = _titleName.preferredHeight;
            _titleName.rectTransform.sizeDelta =
                new Vector2(_titleName.rectTransform.sizeDelta.x, preferredHeightTitle);

            var allHeight = preferredHeightDescription + preferredHeightTitle;
            allHeight += _closeButtonRect.sizeDelta.y;
            allHeight += _upOffset + _bottomOffset;

            _body.sizeDelta = new Vector2(_body.sizeDelta.x, allHeight);
        }
    }
}
