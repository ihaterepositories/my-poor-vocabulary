namespace Utils.Validation.Validators
{
    public class ExtraSpacesValidator : Abstraction.InputValidationChainMember
    {
        public override bool Validate(string input)
        {
            if (input.StartsWith(" ") || input.EndsWith(" ") || input.Contains("  "))
            {
                SendValidationError($"Input contains extra spaces");
                return false;
            }

            return NextValidationChainMember?.Validate(input) ?? true;
        }
    }
}