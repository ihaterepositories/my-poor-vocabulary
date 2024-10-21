using System;
using VocabularyModule.Data.Input.Validation.UI;
using VocabularyModule.Data.Input.Validation.Validators.Interfaces;

namespace VocabularyModule.Data.Input.Validation.Validators
{
    public class ExtraSpacesValidator : InputValidator
    {
        public override bool Validate(string input, string inputPart)
        {
            if (input.Contains("  ") || input.StartsWith(" ") || input.EndsWith(" "))
            {
                SendValidationError($"{inputPart} contains extra spaces");
                return false;
            }

            return NextValidator?.Validate(input, inputPart) ?? true;
        }
    }
}