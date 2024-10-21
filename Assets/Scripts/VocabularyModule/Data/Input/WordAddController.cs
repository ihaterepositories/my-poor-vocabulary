using System;
using UnityEngine;
using UnityEngine.UI;
using VocabularyModule.Data.Input.Validation;
using VocabularyModule.Data.Input.Validation.UI;
using VocabularyModule.Data.Models;
using Zenject;

namespace VocabularyModule.Data.Input
{
    public class WordAddController : MonoBehaviour
    {
        [SerializeField] private InputField originalWordInputField;
        [SerializeField] private InputField translatedWordInputField;
        [SerializeField] private Button addWordButton;
        [SerializeField] private ValidationMessageView validationMessageView;
        
        private VocabularyController _vocabularyController;
        private InputValidationChainExecutor _inputValidationChainExecutor;
        
        public static event Action OnWordAdded;

        [Inject]
        private void Construct(VocabularyController vocabularyController)
        {
            _vocabularyController = vocabularyController;
        }
        
        private void Awake()
        {
            _inputValidationChainExecutor = new InputValidationChainExecutor();
            addWordButton.onClick.AddListener(AddWord);
        }
        
        private void AddWord()
        {
            var word = CreateWord();
            
            if (!ValidateInputPart(word.Original, "Original word") || !ValidateInputPart(word.Translation, "Translated word"))
            {
                validationMessageView.ShowError(GetErrorMessage());
                return;
            }
            
            // TODO: check spelling
            // TODO: check if word already exists in vocabulary
            
            AddWordToVocabulary(word);
            validationMessageView.HideMessage();
            OnWordAdded?.Invoke();
        }
        
        private Word CreateWord()
        {
            var originalWord = originalWordInputField.text;
            var translatedWord = translatedWordInputField.text;
            return new Word(originalWord, translatedWord);
        }

        private bool ValidateInputPart(string input, string inputPart)
        {
            return _inputValidationChainExecutor.Execute(input, inputPart);
        }
        
        private string GetErrorMessage()
        {
            return _inputValidationChainExecutor.LastValidationError;
        }
        
        private void AddWordToVocabulary(Word word)
        {
            _vocabularyController.Vocabulary.Words.Add(word);
            Debug.Log("Word added to vocabulary");
        }
    }
}