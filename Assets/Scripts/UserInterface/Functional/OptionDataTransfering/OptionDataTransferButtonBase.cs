using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Functional.OptionDataTransfering
{
    public abstract class OptionDataTransferButtonBase : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private string optionTransferKey;
        [SerializeField] private string optionTransferValue;

        private void Awake()
        {
            button.onClick.AddListener(SaveOptionData);
            button.onClick.AddListener(InvokeEvent);
        }

        private void SaveOptionData()
        {
            if (optionTransferKey == null || optionTransferValue == null)
            {
                Debug.LogError("OptionTransfer data is null");
                return;
            }
            
            PlayerPrefs.SetString(optionTransferKey, optionTransferValue);
            PlayerPrefs.Save();
        }
        
        protected abstract void InvokeEvent();
    }
}