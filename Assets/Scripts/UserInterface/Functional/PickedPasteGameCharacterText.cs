using Modules.MiniGamesCore.PasteGameModule.Data.Factories;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Functional.OptionDataTransfering;

namespace UserInterface.Functional
{
    public class PickedPasteGameCharacterText : MonoBehaviour
    {
        [SerializeField] private Text pickedPasteGameTopicText;

        private void OnEnable()
        {
            PasteGameCharacterOptionDataTransferButton.OnButtonClicked += SetText;
        }
        
        private void OnDisable()
        {
            PasteGameCharacterOptionDataTransferButton.OnButtonClicked -= SetText;
        }

        private void SetText()
        {
            pickedPasteGameTopicText.text = PasteGameCharactersDescriptionsFactory.GetDescription(PlayerPrefs.GetString("PickedPasteGameCharacter", "any"));
        }
    }
}