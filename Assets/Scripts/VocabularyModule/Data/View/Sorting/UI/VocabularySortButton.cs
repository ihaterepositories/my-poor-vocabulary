using System;
using UnityEngine;
using UnityEngine.UI;

namespace VocabularyModule.Data.View.Sorting.UI
{
    public class VocabularySortButton : MonoBehaviour
    {
        [SerializeField] private int sortType = 0;
        [SerializeField] private Button button;

        public static event Action<int> OnClicked; 
        
        private void Awake()
        {
            button.onClick.AddListener(InvokeSortEvent);
        }

        private void InvokeSortEvent()
        {
            OnClicked?.Invoke(sortType);
        }
    }
}