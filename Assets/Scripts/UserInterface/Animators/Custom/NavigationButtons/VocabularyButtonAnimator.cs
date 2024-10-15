using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UserInterface.Animators.Custom.NavigationButtons
{
    public class VocabularyButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RectTransform mainIcon;
        
        private Vector3 _initialRotationValue;
        private Vector3 _initialScaleValue;
        
        private Vector3 _rotationValueAddition = new(0f, 0f, 10f);
        private readonly float _scaleValueMultiplier = 1.1f;
        private readonly float _animationTime = 0.3f;

        private void Awake()
        {
            _initialRotationValue = mainIcon.eulerAngles;
            _initialScaleValue = mainIcon.localScale;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _rotationValueAddition = -_rotationValueAddition;
            mainIcon.DORotate(_initialRotationValue + _rotationValueAddition, _animationTime);
            mainIcon.DOScale(_initialScaleValue * _scaleValueMultiplier, _animationTime);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mainIcon.DORotate(_initialRotationValue, _animationTime);
            mainIcon.DOScale(_initialScaleValue, _animationTime);
        }
    }
}