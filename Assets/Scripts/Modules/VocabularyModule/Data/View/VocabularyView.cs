using System;
using System.Collections.Generic;
using System.Linq;
using Modules.VocabularyModule.Data.Models;
using Modules.VocabularyModule.Data.View.Sorting;
using Modules.VocabularyModule.Data.View.Sorting.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.VocabularyModule.Data.View
{
    public class VocabularyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private InputField searchInput;
        
        private VocabularyController _vocabularyController;
        private List<Word> _vocabulary;
        private int _lastSortType;
        
        [Inject]
        private void Construct(VocabularyController vocabularyController)
        {
            _vocabularyController = vocabularyController;
        }

        private void Start()
        {
            LoadVocabulary();
            
            _lastSortType = 0;
            searchInput.onValueChanged.AddListener(_ => ShowSearched());
            
            Sort(0);
            Show(FormatWords(_vocabulary));
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
            _vocabulary = _vocabularyController.Vocabulary.GetAll();
            
            if (_vocabulary == null)
                Debug.LogError(gameObject.name+": words are null");
        }

        private void ShowSorted(int sortType)
        {
            searchInput.text = "";
            if (_lastSortType != sortType)
            {
                Sort(sortType);
                _lastSortType = sortType;
            }

            Show(FormatWords(_vocabulary));
        }
        
        private void ShowSearched()
        {
            if (searchInput.text == string.Empty || searchInput.text == " " || searchInput.text == null)
            {
                Show(FormatWords(_vocabulary));
                return;
            }

            var searchedWords = GetSearchedWords();
            Show(FormatWords(searchedWords, true));
        }

        private void Sort(int sortType)
        {
            _vocabulary = VocabularySortFactory.GetSorter(sortType).Sort(_vocabulary);
        }
        
        private List<Word> GetSearchedWords()
        {
            var search = searchInput.text.ToLower();
            return _vocabulary
                .Where(word => 
                    word.Original.ToLower().Contains(search) 
                    || 
                    word.Translation.ToLower().Contains(search))
                .ToList();
        }
        
        private List<string> FormatWords(List<Word> words, bool isHighlightSearched = false)
        {
            var formattedWords = new List<string> { new('-', 96) };

            foreach (var word in words)
            {
                var original = word.Original;
                var translation = word.Translation;
                
                if (isHighlightSearched)
                {
                    original = original.Replace(searchInput.text, $"<u>{searchInput.text}</u>");
                    translation = translation.Replace(searchInput.text, $"<u>{searchInput.text}</u>");
                }
                
                formattedWords.Add($"{original} - {translation}");
                formattedWords.Add(new string('-', 96));
            }

            return formattedWords;
        }
        
        private void Show(List<string> wordsToShow)
        {
            textField.text = String.Join("\n", wordsToShow);
        }
    }
}