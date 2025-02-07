using System;
using Modules.VocabularyModule.Data.Models;
using Modules.VocabularyModule.Data.Validation;
using Modules.VocabularyModule.Data.Validation.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Modules.VocabularyModule.Data.Delete
{
    public class WordDeleteController : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button deleteButton;
        [SerializeField] private Button wordDeleteMenuCallButton;
        [SerializeField] private ValidationMessageView validationMessageView;

        private bool _isWordDeleteMenuActive;
        private Vocabulary _vocabulary;
        private InputValidationChainExecutor _inputValidationChainExecutor;

        public static event Action OnWordDeleted; 

        [Inject]
        private void Construct(VocabularyController vocabularyController)
        {
            _vocabulary = vocabularyController.Vocabulary;
        }

        private void Start()
        {
            _inputValidationChainExecutor = new InputValidationChainExecutor();
            
            // Determine if menu is active by menu`s call button click
            wordDeleteMenuCallButton.onClick.AddListener(()=>_isWordDeleteMenuActive=!_isWordDeleteMenuActive);
            
            // Remove focusing from other buttons to make "Return" key free to use 
            wordDeleteMenuCallButton.onClick.AddListener(() => EventSystem.current.SetSelectedGameObject(null));
            
            deleteButton.onClick.AddListener(TryToDeleteWord);
        }

        private void Update()
        {
            if (_isWordDeleteMenuActive && UnityEngine.Input.GetKeyDown(KeyCode.Return))
            {
                TryToDeleteWord();
            }
        }

        private void TryToDeleteWord()
        {
            if (!ValidateInput())
            {
                validationMessageView.ShowError(GetValidationErrorMessage());
                return;
            }

            if (!_vocabulary.ContainsByOriginalWord(inputField.text))
            {
                validationMessageView.ShowError("This word does not exist in the vocabulary.");
                return;
            }
            
            validationMessageView.HideMessage();
            DeleteWordFromVocabulary();
            OnWordDeleted?.Invoke();
            inputField.text = string.Empty;
        }

        private bool ValidateInput()
        {
            return _inputValidationChainExecutor.Execute(inputField.text, "Original word");
        }
        
        private string GetValidationErrorMessage()
        {
            return _inputValidationChainExecutor.LastValidationError;
        }

        private void DeleteWordFromVocabulary()
        {
            _vocabulary.DeleteWordByOriginal(inputField.text);
        }
    }
}