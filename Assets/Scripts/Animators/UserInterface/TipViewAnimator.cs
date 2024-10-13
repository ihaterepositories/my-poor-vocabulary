using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Animators.UserInterface
{
    public class TipViewAnimator : MonoBehaviour
    {
        [SerializeField] private RectTransform viewBackground;
        [SerializeField] private Text viewText;
        
        private float _initialYPosition;
        private readonly float _onScreenYPosition = 950;
        
        private void Awake()
        {
            _initialYPosition = viewBackground.anchoredPosition.y;
        }
        
        public void Show()
        {
            viewBackground.DOAnchorPosY(_onScreenYPosition, 0.3f)
                .OnComplete(() => 
                    viewText.DOFade(1f, 0.15f));
        }

        public void Hide()
        {
            viewBackground.DOAnchorPosY(_initialYPosition, 0.1f);
            viewText.DOFade(0f, 0.1f);
        }
    }
}