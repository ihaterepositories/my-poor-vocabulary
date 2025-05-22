using System.Collections.Generic;
using System.Linq;
using Modules.PersonalVocabulary;
using Modules.PersonalVocabulary.Data.Input;
using Modules.PersonalVocabulary.Data.Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserInterface.Functional.Widgets
{
    public class WordAddHistoryWidget : MonoBehaviour
    {
        [SerializeField] private Text widgetText;
        [SerializeField] private int historySize;
        
        private Vocabulary _vocabulary;
        
        [Inject]
        private void Construct(VocabularyController vocabularyController)
        {
            _vocabulary = vocabularyController.Vocabulary;
        }
        
        private void Start()
        {
            ShowHistory();
        }

        private void OnEnable()
        {
            WordAddController.OnWordAdded += ShowHistory;
        }
        
        private void OnDisable()
        {
            WordAddController.OnWordAdded -= ShowHistory;
        }
        
        private void ShowHistory()
        {
            widgetText.text = string.Join("\n", LoadHistory());
        }

        private List<string> LoadHistory()
        {
            return _vocabulary
                .GetSortedByNewest()
                .Take(historySize)
                .Select(word => word.Original + " -> " + word.AddingDate.ToShortDateString())
                .ToList();
        }
    }
}