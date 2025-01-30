using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Functional
{
    public class OptionDataTransferButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private string optionTransferKey;

        private void Awake()
        {
            button.onClick.AddListener(SaveOptionData);
        }

        private void SaveOptionData()
        {
            if (optionTransferKey == null)
            {
                Debug.LogError("OptionTransferKey is not set");
                return;
            }
            
            PlayerPrefs.SetString(optionTransferKey, "Empty data received");
            PlayerPrefs.Save();
        }
    }
}