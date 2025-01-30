using UnityEngine;
using UnityEngine.EventSystems;

namespace UserInterface.Functional.TipsCore
{
    public class TipProvider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string tip;
        [SerializeField] private TipView tipView;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            tipView.Show(tip);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tipView.Hide();
        }
    }
}