using System;
using VocabularyModule.Data.Input.Validation.UI;
using VocabularyModule.Data.Input.Validation.Validators.Interfaces;

namespace VocabularyModule.Data.Input.Validation.Validators
{
    public class EmptyInputValidator : InputValidator
    {
        public override bool Validate(string input, string inputPart)
        {
            if (string.IsNullOrEmpty(input))
            {
                SendValidationError($"{inputPart} is empty");
                return false;
            }
            
            return NextValidator?.Validate(input, inputPart) ?? true;
        }
    }
}