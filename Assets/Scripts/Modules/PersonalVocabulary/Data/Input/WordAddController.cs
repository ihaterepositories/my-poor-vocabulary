using System;
using System.Collections.Generic;
using Constants;
using Modules.PersonalVocabulary.Data.Models;
using Modules.ScoreModule;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UserInterface.Functional;
using Utils.Validation;
using Zenject;

namespace Modules.PersonalVocabulary.Data.Input
{
    // TODO: refactor
    public class WordAddController : MonoBehaviour
    {
        [SerializeField] private InputField originalWordInputField;
        [SerializeField] private InputField translatedWordInputField;
        [SerializeField] private Button addWordButton;
        [SerializeField] private ActionResultMessageView actionResultMessageView;
        [SerializeField] private Button addWordPanelCallButton;
        
        private VocabularyController _vocabularyController;
        private InputValidator _inputValidator;
        private ExpController _expController;
        
        private bool _isPanelActive;
        
        public static event Action OnWordAdded;

        [Inject]
        private void Construct(
            VocabularyController vocabularyController,
            ExpController expController)
        {
            _vocabularyController = vocabularyController;
            _expController = expController;
        }
        
        private void Awake()
        {
            _inputValidator = new InputValidator();
            
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
                actionResultMessageView.ShowError(GetValidationErrorMessage());
                return;
            }
            
            if (!_vocabularyController.Vocabulary.CheckIfExistsByOriginalWord(originalWordInputField.text))
            {
                var word = CreateWord();
                AddWordToVocabulary(word);
            }
            else
            {
                var word = _vocabularyController.Vocabulary.GetByOriginal(originalWordInputField.text);

                if (!word.CheckForNewTranslationAddAbility(translatedWordInputField.text))
                {
                    actionResultMessageView.ShowError($"Translation already exists or translations count is maximum ({AppConstants.MaxTranslationsPerWord}).");
                    return;
                }
                
                word.AddTranslation(translatedWordInputField.text);
            }
            
            ClearInputs();
            actionResultMessageView.HideMessage();
            _expController.AddExp(AppConstants.ExpPerAddedWord);
            OnWordAdded?.Invoke();
        }

        private bool ValidateInputs()
        {
            return ValidateInputPart(originalWordInputField.text, "Original word") &&
                   ValidateInputPart(translatedWordInputField.text, "Translated word");
        }
        
        private bool ValidateInputPart(string input, string inputPart)
        {
            return _inputValidator.Validate(input);
        }
        
        private string GetValidationErrorMessage()
        {
            return _inputValidator.LastValidationErrorDescription;
        }
        
        private Word CreateWord()
        {
            var originalWord = originalWordInputField.text;
            var translatedWord = translatedWordInputField.text;
            return new Word(originalWord, new List<string>{translatedWord});
        }
        
        private void AddWordToVocabulary(Word word)
        {
            _vocabularyController.Vocabulary.Add(word);
        }

        private void ClearInputs()
        {
            originalWordInputField.text = "";
            translatedWordInputField.text = "";
        }
    }
}