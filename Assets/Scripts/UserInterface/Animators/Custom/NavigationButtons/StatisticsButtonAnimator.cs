using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UserInterface.Animators.Custom.NavigationButtons
{
    public class StatisticsButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RectTransform mainIcon;
        [SerializeField] private RectTransform backgroundIcon;
        
        private Vector3 _mainInitialRotationValue;
        private Vector3 _backgroundInitialScaleValue;
        
        private readonly Vector3 _rotationValueAddition = new(0f, 0f, 15f);
        private readonly float _scaleValueMultiplier = 1.1f;
        
        private readonly float _animationTime = 0.3f;
        
        private void Awake()
        {
            _mainInitialRotationValue = mainIcon.eulerAngles;
            _backgroundInitialScaleValue = backgroundIcon.localScale;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            mainIcon.DORotate(_mainInitialRotationValue + _rotationValueAddition, _animationTime);
            backgroundIcon.DOScale(_backgroundInitialScaleValue * _scaleValueMultiplier, _animationTime);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mainIcon.DORotate(_mainInitialRotationValue, _animationTime);
            backgroundIcon.DOScale(_backgroundInitialScaleValue, _animationTime);
        }
    }
}