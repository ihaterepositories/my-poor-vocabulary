using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VocabularyModule.Data.Models;
using VocabularyModule.Data.View.Sorting;
using VocabularyModule.Data.View.Sorting.UI;
using Zenject;

namespace VocabularyModule.Data.View
{
    public class VocabularyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textField;
        
        private VocabularyController _vocabularyController;
        private List<Word> _words;
        
        [Inject]
        private void Construct(VocabularyController vocabularyController)
        {
            _vocabularyController = vocabularyController;
        }

        private void Start()
        {
            LoadVocabulary();
            ShowSorted(0);
        }

        private void OnEnable()
        {
            VocabularySortButton.OnClicked += ShowSorted;
        }
        
        private void OnDisable()
        {
            VocabularySortButton.OnClicked -= ShowSorted;
        }

        private void LoadVocabulary()
        {
            _words = _vocabularyController.Vocabulary.Words;
            
            if (_words == null)
                Debug.LogError(gameObject.name+": words are null");
        }

        private void ShowSorted(int sortType)
        {
            Sort(sortType);
            Show();
        }

        private void Sort(int sortType)
        {
            _words = VocabularySortFactory.GetSorter(sortType).Sort(_words);
        }
        
        private void Show()
        {
            var formattedVocabulary = new List<string> { new('-', 96) };

            foreach (var word in _words)
            {
                var original = word.Original;
                var translation = word.Translation;
                
                formattedVocabulary.Add($"{original} - {translation}");
                formattedVocabulary.Add(new string('-', 96));
            }

            textField.text = String.Join("\n", formattedVocabulary);
        }
    }
}