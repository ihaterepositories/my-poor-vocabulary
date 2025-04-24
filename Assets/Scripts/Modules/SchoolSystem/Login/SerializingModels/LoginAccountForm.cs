using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Utils.Validation;

namespace Modules.SchoolSystem.Login.SerializingModels
{
    [Serializable]
    public class LoginAccountForm
    {
        public InputField emailField;
        public InputField passwordField;
        public Button signInButton;
        
        public string ValidationErrorDescription { get; private set; }
        
        public bool IsFieldsValid()
        {
            var validator = new InputValidator();
            var result = validator.ValidateForMany(
                new List<string>{emailField.text, passwordField.text},
                new ValidatorConfig
                {
                    UseEmptyValidator = true,
                    UseExtraSpacesValidator = true
                });

            if (!result) ValidationErrorDescription = validator.LastValidationErrorDescription;
            return result;
        }
    }
}