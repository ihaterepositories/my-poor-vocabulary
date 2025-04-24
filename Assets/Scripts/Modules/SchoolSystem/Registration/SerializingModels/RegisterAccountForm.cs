using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils.JsonBuilding;
using Utils.JsonBuilding.SerializingModels;
using Utils.Validation;

namespace Modules.SchoolSystem.Registration.SerializingModels
{
    [Serializable]
    public class RegisterAccountForm
    {
        [SerializeField] private List<JsonReadyInputField> jsonReadyInputFields;
        public string ValidationErrorDescription { get; private set; }

        public string ToJson()
        {
            return JsonBuilder.BuildJsonFromJsonReadyInputs(jsonReadyInputFields);
        }

        public bool IsFieldsValid()
        {
            var validator = new InputValidator();
            var result = validator.ValidateForMany(
                new List<string>(jsonReadyInputFields.Select(x => x.InputField.text)),
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