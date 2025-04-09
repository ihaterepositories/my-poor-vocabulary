using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Animators.Generic
{
    public class VerticalButtonPointerAnimator : MonoBehaviour
    {
        [SerializeField] private List<Button> buttons;

        private void Start()
        {
            foreach (var button in buttons)
            {
                button.onClick.AddListener(() => ChangeYPosition(button.transform.localPosition.y));
            }
        }

        private void ChangeYPosition(float yPosition)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, yPosition, gameObject.transform.localPosition.z);
        }
    }
}