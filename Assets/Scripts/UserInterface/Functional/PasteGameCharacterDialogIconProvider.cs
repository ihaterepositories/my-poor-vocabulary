using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.SerializingModels;

namespace UserInterface.Functional
{
    public class PasteGameCharacterDialogIconProvider : MonoBehaviour
    {
        [SerializeField] private List<SerializableKeyValue<Sprite>> iconsContainer;
        [SerializeField] private Image icon;
        [SerializeField] private Canvas canvas;

        private void Start()
        {
            string characterName = PlayerPrefs.GetString("PickedPasteGameCharacter", "Medieval knight");
            Sprite iconSprite = iconsContainer.FirstOrDefault(i => i.key == characterName)?.value ?? iconsContainer[0].value;
            icon.sprite = iconSprite;
        }
    }
}