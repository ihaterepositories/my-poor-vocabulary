using System;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Functional
{
    public class NumericDataTransferButton : MonoBehaviour
    {
        [SerializeField] private int numberToTransfer = 0;
        [SerializeField] private Button button;

        public event Action<int> OnTransferData;
        
        private void Awake()
        {
            button.onClick.AddListener(() => OnTransferData?.Invoke(numberToTransfer));
        }
    }
}