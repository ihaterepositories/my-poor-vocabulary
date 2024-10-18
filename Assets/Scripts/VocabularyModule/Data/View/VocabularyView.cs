using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
using TMPro;
using UnityEngine;
using VocabularyModule.Data.Models;
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
            LoadWords();
            Show();
        }

        private void LoadWords()
        {
            _words = _vocabularyController.Vocabulary.Words;
            
            if (_words == null)
                Debug.LogError(gameObject.name+": words are null");
        }

        private void Show()
        {
            var formattedVocabulary = new List<string> { new string('-', 96) };

            foreach (var word in _words)
            {
                string original = word.Original;
                string translation = word.Translation;
                
                formattedVocabulary.Add($"{original} - {translation}");
                formattedVocabulary.Add(new string('-', 96));
            }

            textField.text = String.Join("\n", formattedVocabulary);
        }
    }
}