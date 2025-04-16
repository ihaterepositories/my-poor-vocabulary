using Constants;

namespace Utils.Validation.Validators
{
    public class VocabularyWordLengthValidator : Abstraction.InputValidationChainMember
    {
        public override bool Validate(string input)
        {
            if (input.Length > AppConstants.MaxWordLength)
            {
                var firstLetters = input.Substring(0, 3);
                SendValidationError($"{firstLetters}... is too long. {AppConstants.MaxWordLength} characters is maximum.");
                return false;
            }

            return NextValidationChainMember?.Validate(input) ?? true;
        }
    }
}