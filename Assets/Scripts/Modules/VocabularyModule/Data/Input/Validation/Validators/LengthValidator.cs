using Constants;
using Modules.VocabularyModule.Data.Input.Validation.Validators.Interfaces;

namespace Modules.VocabularyModule.Data.Input.Validation.Validators
{
    public class LengthValidator : InputValidator
    {
        public override bool Validate(string input, string inputPart)
        {
            if (input.Length > AppConstants.MaxWordLength)
            {
                SendValidationError($"{inputPart} is too long");
                return false;
            }

            return NextValidator?.Validate(input, inputPart) ?? true;
        }
    }
}