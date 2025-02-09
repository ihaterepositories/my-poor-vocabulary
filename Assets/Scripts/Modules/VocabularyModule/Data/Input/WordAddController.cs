using System;
using Constants;
using Modules.ScoreModule;
using Modules.VocabularyModule.Data.Models;
using Modules.VocabularyModule.Data.Validation;
using Modules.VocabularyModule.Data.Validation.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Modules.VocabularyModule.Data.Input
{
    // TODO: refactor
    public class WordAddController : MonoBehaviour
    {
        [SerializeField] private InputField originalWordInputField;
        [SerializeField] private InputField translatedWordInputField;
        [SerializeField] private Button addWordButton;
        [SerializeField] private ValidationMessageView validationMessageView;
        [SerializeField] private Button addWordPanelCallButton;
        
        private VocabularyController _vocabularyController;
        private InputValidationChainExecutor _inputValidationChainExecutor;
        private ScoreController _scoreController;
        
        private bool _isPanelActive;
        
        public static event Action OnWordAdded;

        [Inject]
        private void Construct(
            VocabularyController vocabularyController,
            ScoreController scoreController)
        {
            _vocabularyController = vocabularyController;
            _scoreController = scoreController;
        }
        
        private void Awake()
        {
            _inputValidationChainExecutor = new InputValidationChainExecutor();
            
            // Remove focusing from other buttons to make "Return" key free to use 
            addWordPanelCallButton.onClick.AddListener(() => EventSystem.current.SetSelectedGameObject(null));
            
            addWordPanelCallButton.onClick.AddListener(() => _isPanelActive = !_isPanelActive);
            addWordButton.onClick.AddListener(AddWord);
        }

        private void Update()
        {
            if (_isPanelActive)
            {
                if (UnityEngine.Input.GetKeyDown(KeyCode.Return))
                {
                    AddWord();
                }
            }
        }

        private void AddWord()
        {
            if (!ValidateInputs())
            {
                // TODO: spellcheck
                validationMessageView.ShowError(GetValidationErrorMessage());
                return;
            }
            
            if (!_vocabularyController.Vocabulary.ContainsByOriginalWord(originalWordInputField.text))
            {
                var word = CreateWord();
                AddWordToVocabulary(word);
            }
            else
            {
                var word = _vocabularyController.Vocabulary.GetByOriginal(originalWordInputField.text);
                AddNewTranslationToWord(word, translatedWordInputField.text);
            }
            
            ClearInputs();
            validationMessageView.HideMessage();
            _scoreController.AddExp(AppConstants.ExpPerAddedWord);
            OnWordAdded?.Invoke();
        }

        private bool ValidateInputs()
        {
            return ValidateInputPart(originalWordInputField.text, "Original word") &&
                   ValidateInputPart(translatedWordInputField.text, "Translated word");
        }
        
        private bool ValidateInputPart(string input, string inputPart)
        {
            return _inputValidationChainExecutor.Execute(input, inputPart);
        }
        
        private string GetValidationErrorMessage()
        {
            return _inputValidationChainExecutor.LastValidationError;
        }
        
        private Word CreateWord()
        {
            var originalWord = originalWordInputField.text;
            var translatedWord = translatedWordInputField.text;
            return new Word(originalWord, translatedWord);
        }
        
        private void AddWordToVocabulary(Word word)
        {
            _vocabularyController.Vocabulary.Add(word);
        }
        
        private void AddNewTranslationToWord(Word word, string translation)
        {
            word.Translations.Add(translation);
        }

        private void ClearInputs()
        {
            originalWordInputField.text = "";
            translatedWordInputField.text = "";
        }
    }
}