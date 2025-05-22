using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UserInterface.Functional;
using Utils.Validation;
using Zenject;

namespace Modules.PersonalVocabulary.Data.Delete
{
    public class WordDeleteController : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button deleteButton;
        [SerializeField] private Button wordDeleteMenuCallButton;
        [FormerlySerializedAs("validationMessageView")] [SerializeField] private ActionResultMessageView actionResultMessageView;

        private bool _isWordDeleteMenuActive;
        private Models.Vocabulary _vocabulary;
        private InputValidator _validator;

        public static event Action OnWordDeleted; 

        [Inject]
        private void Construct(VocabularyController vocabularyController)
        {
            _vocabulary = vocabularyController.Vocabulary;
        }

        private void Start()
        {
            _validator = new InputValidator();
            
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
                actionResultMessageView.ShowError(GetValidationErrorMessage());
                return;
            }

            if (!_vocabulary.CheckIfExistsByOriginalWord(inputField.text))
            {
                actionResultMessageView.ShowError("This word does not exist in the vocabulary.");
                return;
            }
            
            actionResultMessageView.HideMessage();
            DeleteWordFromVocabulary();
            OnWordDeleted?.Invoke();
            inputField.text = string.Empty;
        }

        private bool ValidateInput()
        {
            return _validator.Validate(inputField.text);
        }
        
        private string GetValidationErrorMessage()
        {
            return _validator.LastValidationErrorDescription;
        }

        private void DeleteWordFromVocabulary()
        {
            _vocabulary.DeleteWordByOriginal(inputField.text);
        }
    }
}