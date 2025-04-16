using System.Linq;
using Utils.Validation.Validators.Abstraction;

namespace Utils.Validation.Validators
{
    public class AlphabeticWordValidator : InputValidationChainMember
    {
        public override bool Validate(string input)
        {
            var chars = input
                .Where(c => c != ' ')
                .ToArray();
            
            foreach (var ch in chars)
            {
                if (!char.IsLetter(ch) && ch != '\'' && ch != '`')
                {
                    SendValidationError($"{input} contains non-alphabetic characters");
                    return false;
                }
            }

            return NextValidationChainMember?.Validate(input) ?? true;
        }
    }
}