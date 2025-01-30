using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Functional
{
    public class PickedPasteGameTopicText : MonoBehaviour
    {
        [SerializeField] private Text pickedPasteGameTopicText;

        private void Update()
        {
            pickedPasteGameTopicText.text = "picked paste game topic: " + PlayerPrefs.GetString("PickedPasteGameTopic", "any");
        }
    }
}