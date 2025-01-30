using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Animators.Custom
{
    public class TipViewAnimator : MonoBehaviour
    {
        [SerializeField] private RectTransform viewBackground;
        [SerializeField] private Text viewText;
        
        private float _initialYPosition;
        private readonly float _onScreenYPosition = 1000;
        
        private void Awake()
        {
            _initialYPosition = viewBackground.anchoredPosition.y;
        }

        private void OnDisable()
        {
            viewBackground.DOKill();
            viewText.DOKill();
        }

        public void Show()
        {
            viewBackground.DOAnchorPosY(_onScreenYPosition, 0.2f)
                .OnComplete(() => 
                    viewText.DOFade(1f, 0.1f));
        }

        public void Hide()
        {
            viewBackground.DOAnchorPosY(_initialYPosition, 0.1f);
            viewText.DOFade(0f, 0.1f);
        }
    }
}