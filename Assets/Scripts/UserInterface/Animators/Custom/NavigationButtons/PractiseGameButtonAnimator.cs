using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UserInterface.Animators.Custom.NavigationButtons
{
    public class PractiseGameButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RectTransform mainIcon;
        [SerializeField] private List<RectTransform> backgroundIcons;
        
        private Vector3 _mainInitialScaleValue;
        private readonly Vector3 _mainScaleValueAddition = new(-0.05f, -0.05f, -0.05f);
        
        private List<Vector3> _backgroundInitialScaleValues;
        private readonly Vector3 _backgroundScaleValueAddition = new(0.1f, 0.1f, 0.1f);
        
        private List<Vector3> _backgroundInitialRotationValues;
        private readonly float _backgroundMinRotationValueAddition = -20f;
        private readonly float _backgroundMaxRotationValueAddition = 20f;
        
        private readonly float _animationTime = 0.3f;

        
        private void Awake()
        {
            _mainInitialScaleValue = mainIcon.localScale;
            
            _backgroundInitialScaleValues = new List<Vector3>();
            _backgroundInitialRotationValues = new List<Vector3>();
            foreach (var backgroundIcon in backgroundIcons)
            {
                _backgroundInitialScaleValues.Add(backgroundIcon.localScale);
                _backgroundInitialRotationValues.Add(backgroundIcon.eulerAngles);
            }
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            mainIcon.DOScale(_mainInitialScaleValue + _mainScaleValueAddition, _animationTime);
            
            for (var i = 0; i < backgroundIcons.Count; i++)
            {
                var randomRotationValue = Random.Range(_backgroundMinRotationValueAddition, _backgroundMaxRotationValueAddition);
                backgroundIcons[i].DOScale(
                    _backgroundInitialScaleValues[i] + _backgroundScaleValueAddition, 
                    _animationTime);
                backgroundIcons[i].DORotate(
                    _backgroundInitialRotationValues[i] + new Vector3(0f, 0f, randomRotationValue), 
                    _animationTime);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mainIcon.DOScale(_mainInitialScaleValue, _animationTime);
            
            for (var i = 0; i < backgroundIcons.Count; i++)
            {
                backgroundIcons[i].DOScale(_backgroundInitialScaleValues[i], _animationTime);
                backgroundIcons[i].DORotate(_backgroundInitialRotationValues[i], _animationTime);
            }
        }
    }
}
