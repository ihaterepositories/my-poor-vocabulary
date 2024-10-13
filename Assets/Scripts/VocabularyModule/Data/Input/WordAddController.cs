using System;
using UnityEngine;
using UnityEngine.UI;
using VocabularyModule.Data.Models;
using Zenject;

namespace VocabularyModule.Data.Input
{
    public class WordAddController : MonoBehaviour
    {
        [SerializeField] private InputField originalWordInputField;
        [SerializeField] private InputField translatedWordInputField;
        [SerializeField] private Button addWordButton;
        
        private VocabularyController _vocabularyController;
        
        public static event Action OnWordAdded;

        [Inject]
        private void Construct(VocabularyController vocabularyController)
        {
            _vocabularyController = vocabularyController;
        }
        
        private void Awake()
        {
            addWordButton.onClick.AddListener(AddWord);
        }
        
        private void AddWord()
        {
            var word = CreateWord();
            Debug.Log("Created word: " + word.Original + " - " + word.Translation);

            // Get and show error message
            
            AddWordToVocabulary(word);
            OnWordAdded?.Invoke();
        }
        
        private Word CreateWord()
        {
            var originalWord = originalWordInputField.text;
            var translatedWord = translatedWordInputField.text;
            return new Word(originalWord, translatedWord);
        }

        private bool CheckWord(Word word)
        {
            // InputErrorChecker have to check if the word is correct
            return true;
        }
        
        private string GetErrorMessage()
        {
            // InputErrorChecker have to return error message
            return "";
        }
        
        private void AddWordToVocabulary(Word word)
        {
            _vocabularyController.Vocabulary.Words.Add(word);
        }
    }
}