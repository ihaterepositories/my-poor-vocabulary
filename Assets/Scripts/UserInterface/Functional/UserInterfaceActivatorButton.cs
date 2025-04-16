using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Functional
{
    public class UserInterfaceActivatorButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private bool isActiveAtStart;
        [SerializeField] private List<RectTransform> uiElementsToShow;
        [SerializeField] private List<RectTransform> uiElementsToHide;

        private void Start()
        {
            foreach (RectTransform uiElement in uiElementsToShow)
            {
                uiElement.gameObject.SetActive(isActiveAtStart);
            }
            
            button.onClick.AddListener(ShowElements);
        }

        private void ShowElements()
        {
            foreach (RectTransform uiElementToShow in uiElementsToShow)
            {
                if (!uiElementToShow.gameObject.activeInHierarchy)
                    uiElementToShow.gameObject.SetActive(true);
            }

            foreach (var uiElementToHide in uiElementsToHide)
            {
                if (uiElementToHide.gameObject.activeInHierarchy)
                    uiElementToHide.gameObject.SetActive(false);
            }
        }
    }
}