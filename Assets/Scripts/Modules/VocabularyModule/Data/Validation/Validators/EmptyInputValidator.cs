using Modules.VocabularyModule.Data.Validation.Validators.Interfaces;

namespace Modules.VocabularyModule.Data.Validation.Validators
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