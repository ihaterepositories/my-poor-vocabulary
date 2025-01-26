using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UserInterface.Animators.Custom.NavigationButtons
{
    public class PasteGameButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RectTransform mainIcon;
        [SerializeField] private RectTransform backPasteIcon;
        [SerializeField] private RectTransform frontPasteIcon;
        
        private Vector3 _mainIconInitialScale;
        private Vector3 _backPasteIconInitialScale;
        private Vector3 _frontPasteIconInitialScale;
        
        private readonly float _scaleMultiplier = 1.05f;
        private readonly float _animationDuration = 0.2f;
        
        private void Awake()
        {
            _mainIconInitialScale = mainIcon.localScale;
            _backPasteIconInitialScale = backPasteIcon.localScale;
            _frontPasteIconInitialScale = frontPasteIcon.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            mainIcon.DOScale(_mainIconInitialScale * _scaleMultiplier, _animationDuration);
            backPasteIcon.DOScale(_backPasteIconInitialScale / _scaleMultiplier, _animationDuration);
            frontPasteIcon.DOScale(_frontPasteIconInitialScale / _scaleMultiplier, _animationDuration);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mainIcon.DOScale(_mainIconInitialScale, _animationDuration);
            backPasteIcon.DOScale(_backPasteIconInitialScale, _animationDuration);
            frontPasteIcon.DOScale(_frontPasteIconInitialScale, _animationDuration);
        }
    }
}