using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Animators.Generic
{
    public class InteractableButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private bool isIncrease = true;
        [SerializeField] private float scaleMultiplier = 1.1f;
        
        [SerializeField] private bool isRotate = true;
        [SerializeField] private float rotateAngle = 10f;
        
        [SerializeField] private RectTransform rectTransform;
        
        private Vector3 _initialScale;
        private Vector3 _initialRotation;
        
        private void Awake()
        {
            _initialScale = rectTransform.localScale;
            _initialRotation = rectTransform.localEulerAngles;
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isIncrease)
                rectTransform.DOScale(_initialScale * scaleMultiplier, 0.2f);
            if (isRotate)
                rectTransform.DORotate(_initialRotation + new Vector3(0, 0, rotateAngle), 0.2f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isIncrease)
                rectTransform.DOScale(_initialScale, 0.2f);
            if (isRotate)
                rectTransform.DORotate(_initialRotation, 0.2f);
        }
    }
}