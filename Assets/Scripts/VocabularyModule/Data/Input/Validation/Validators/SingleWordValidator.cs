using VocabularyModule.Data.Input.Validation.Validators.Interfaces;

namespace VocabularyModule.Data.Input.Validation.Validators
{
    public class SingleWordValidator : InputValidator
    {
        public override bool Validate(string input, string inputPart)
        {
            if (input.Contains(" "))
            {
                SendValidationError($"{inputPart} contains spaces");
                return false;
            }

            return NextValidator?.Validate(input, inputPart) ?? true;
        }
    }
}