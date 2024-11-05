using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UserInterface.Animators.Custom.NavigationButtons
{
    public class CorrectionGameButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RectTransform mainIcon;
        [SerializeField] private RectTransform closeIcon;
        
        private Vector3 mainIconInitialScale;
        private Vector3 closeIconInitialScale;
        
        private readonly float scaleMultiplier = 1.05f;
        private readonly float animationDuration = 0.2f;

        private void Awake()
        {
            mainIconInitialScale = mainIcon.localScale;
            closeIconInitialScale = closeIcon.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            mainIcon.DOScale(mainIconInitialScale * scaleMultiplier, animationDuration);
            closeIcon.DOScale(closeIconInitialScale * scaleMultiplier, animationDuration);
            closeIcon.DOLocalRotate(new Vector3(0f, 0f, 360f), animationDuration, RotateMode.FastBeyond360);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mainIcon.DOScale(mainIconInitialScale, animationDuration);
            closeIcon.DORotate(Vector3.zero, animationDuration, RotateMode.FastBeyond360);
        }
    }
}