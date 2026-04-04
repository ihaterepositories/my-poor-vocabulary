using Modules.MiniGames.PasteGame.Data.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.MiniGames.PasteGame.UserInterface
{
    public class PickedPasteGameCharacterText : MonoBehaviour
    {
        [SerializeField] private Text pickedPasteGameTopicText;

        private void OnEnable()
        {
            PasteGameCharacterToPlayerPrefsButton.OnButtonClicked += SetText;
        }
        
        private void OnDisable()
        {
            PasteGameCharacterToPlayerPrefsButton.OnButtonClicked -= SetText;
        }

        private void SetText()
        {
            pickedPasteGameTopicText.text = PasteGameCharactersDescriptionsFactory.GetDescription(PlayerPrefs.GetString("PickedPasteGameCharacter", "any"));
        }
    }
}