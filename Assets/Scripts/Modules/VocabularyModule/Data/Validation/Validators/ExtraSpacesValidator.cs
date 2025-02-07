using Modules.VocabularyModule.Data.Validation.Validators.Interfaces;

namespace Modules.VocabularyModule.Data.Validation.Validators
{
    public class ExtraSpacesValidator : InputValidator
    {
        public override bool Validate(string input, string inputPart)
        {
            if (input.StartsWith(" ") || input.EndsWith(" ") || input.Contains("  "))
            {
                SendValidationError($"{inputPart} contains extra spaces");
                return false;
            }

            return NextValidator?.Validate(input, inputPart) ?? true;
        }
    }
}