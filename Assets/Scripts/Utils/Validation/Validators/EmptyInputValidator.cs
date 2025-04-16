using Utils.Validation.Validators.Abstraction;

namespace Utils.Validation.Validators
{
    public class EmptyInputValidator : InputValidationChainMember
    {
        public override bool Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                SendValidationError($"Input is empty");
                return false;
            }
            
            return NextValidationChainMember?.Validate(input) ?? true;
        }
    }
}