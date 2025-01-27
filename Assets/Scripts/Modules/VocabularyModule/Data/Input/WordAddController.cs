using System;
using Constants;
using Modules.ScoreModule;
using Modules.VocabularyModule.Data.Input.Validation;
using Modules.VocabularyModule.Data.Input.Validation.UI;
using Modules.VocabularyModule.Data.Models;
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
            
            // Remove focusing from button to make "Return" key free to use 
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
            var word = CreateWord();
            
            if (!ValidateInputPart(word.Original, "Original word") || !ValidateInputPart(word.Translation, "Translated word"))
            {
                validationMessageView.ShowError(GetErrorMessage());
                return;
            }
            
            // TODO: check spelling
            // ??? TODO: check if word already exists in vocabulary
            
            AddWordToVocabulary(word);
            ClearInputs();
            validationMessageView.HideMessage();
            _scoreController.AddExp(AppConstants.ExpPerAddedWord);
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
            _vocabularyController.Vocabulary.Add(word);
        }

        private void ClearInputs()
        {
            originalWordInputField.text = "";
            translatedWordInputField.text = "";
        }
    }
}