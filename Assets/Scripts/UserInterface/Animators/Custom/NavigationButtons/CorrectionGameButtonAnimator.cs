using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UserInterface.Animators.Custom.NavigationButtons
{
    public class CorrectionGameButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RectTransform mainIcon;
        [SerializeField] private RectTransform closeIcon;
        
        private Vector3 _mainIconInitialScale;
        private Vector3 _closeIconInitialScale;
        
        private readonly float _scaleMultiplier = 1.05f;
        private readonly float _animationDuration = 0.2f;

        private void Awake()
        {
            _mainIconInitialScale = mainIcon.localScale;
            _closeIconInitialScale = closeIcon.localScale;
        }

        private void OnDisable()
        {
            mainIcon.DOKill();
            closeIcon.DOKill();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            mainIcon.DOScale(_mainIconInitialScale * _scaleMultiplier, _animationDuration);
            closeIcon.DOScale(_closeIconInitialScale * _scaleMultiplier, _animationDuration);
            closeIcon.DOLocalRotate(new Vector3(0f, 0f, 360f), _animationDuration, RotateMode.FastBeyond360);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mainIcon.DOScale(_mainIconInitialScale, _animationDuration);
            closeIcon.DORotate(Vector3.zero, _animationDuration, RotateMode.FastBeyond360);
        }
    }
}