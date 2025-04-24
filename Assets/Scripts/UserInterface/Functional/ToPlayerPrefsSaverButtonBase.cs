using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Functional
{
    public abstract class ToPlayerPrefsSaverButtonBase : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private string optionTransferKey;
        [SerializeField] private string optionTransferValue;

        private void Awake()
        {
            button.onClick.AddListener(SaveOptionData);
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
            OnDataSaved();
        }
        
        protected abstract void OnDataSaved();
    }
}