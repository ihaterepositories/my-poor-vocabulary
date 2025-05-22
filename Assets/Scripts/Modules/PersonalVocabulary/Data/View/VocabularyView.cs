using System;
using System.Collections.Generic;
using System.Linq;
using Modules.PersonalVocabulary.Data.Delete;
using Modules.PersonalVocabulary.Data.Models;
using Modules.PersonalVocabulary.Data.View.Sorting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Functional;
using Zenject;

namespace Modules.PersonalVocabulary.Data.View
{
    public class VocabularyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private InputField searchInput;
        [SerializeField] private List<NumericDataTransferButton> sortOptionButtons;
        
        private Models.Vocabulary _vocabulary;
        private List<Word> _vocabularyWords;
        private int _lastSortType;
        
        [Inject]
        private void Construct(VocabularyController vocabularyController)
        {
            _vocabulary = vocabularyController.Vocabulary;
        }

        private void Start()
        {
            LoadVocabulary();
            
            _lastSortType = 0;
            searchInput.onValueChanged.AddListener(_ => ShowSearched());
            
            Sort(0);
            ShowWords(FormatWords(_vocabularyWords));
        }

        private void OnEnable()
        {
            foreach (var b in sortOptionButtons)
            {
                b.OnTransferData += ShowSorted;
            }
            
            WordDeleteController.OnWordDeleted += RefreshView;
        }
        
        private void OnDisable()
        {
            foreach (var b in sortOptionButtons)
            {
                b.OnTransferData -= ShowSorted;
            }
            
            WordDeleteController.OnWordDeleted -= RefreshView;
        }
        
        private void RefreshView()
        {
            LoadVocabulary();
            ShowWords(FormatWords(_vocabularyWords));
        }

        private void LoadVocabulary()
        {
            _vocabularyWords = _vocabulary.GetAll();
        }

        private void ShowSorted(int sortType)
        {
            searchInput.text = "";
            if (_lastSortType != sortType)
            {
                Sort(sortType);
                _lastSortType = sortType;
            }

            ShowWords(FormatWords(_vocabularyWords));
        }
        
        private void ShowSearched()
        {
            if (searchInput.text == string.Empty || searchInput.text == " " || searchInput.text == null)
            {
                return;
            }

            var searchedWords = GetSearchedWords();
            ShowWords(FormatWords(searchedWords, true));
        }

        private void Sort(int sortType)
        {
            _vocabularyWords = VocabularySortFactory.GetSorter(sortType).Sort(_vocabularyWords);
        }
        
        private List<Word> GetSearchedWords()
        {
            var search = searchInput.text.ToLower();
            return _vocabularyWords
                .Where(word => 
                    word.Original.ToLower().Contains(search) || 
                    word.Translations.Any(t => t.ToLower().Contains(search)))
                .ToList();
        }
        
        private List<string> FormatWords(List<Word> words, bool isHighlightSearched = false)
        {
            var formattedWords = new List<string> { new('-', 96) };

            foreach (var word in words)
            {
                var original = word.Original;
                var translations = word.Translations;
                
                if (isHighlightSearched)
                {
                    original = original.Replace(searchInput.text, $"<u>{searchInput.text}</u>");
                    translations = translations.Select(t => t.Replace(searchInput.text, $"<u>{searchInput.text}</u>")).ToList();
                }
                
                var formattedWord = $"{original} - ";
                foreach (var t in translations)
                {
                    formattedWord += $"{t}, ";
                }
                formattedWord = formattedWord.TrimEnd(',', ' ');
                
                formattedWords.Add(formattedWord);
                formattedWords.Add(new string('-', 96));
            }

            return formattedWords;
        }
        
        private void ShowWords(List<string> wordsToShow)
        {
            textField.text = String.Join("\n", wordsToShow);
        }
    }
}