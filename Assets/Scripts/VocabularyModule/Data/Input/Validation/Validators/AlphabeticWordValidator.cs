using System;
using System.Linq;
using VocabularyModule.Data.Input.Validation.UI;
using VocabularyModule.Data.Input.Validation.Validators.Interfaces;

namespace VocabularyModule.Data.Input.Validation.Validators
{
    public class AlphabeticWordValidator : InputValidator
    {
        public override bool Validate(string input, string inputPart)
        {
            var chars = input
                .Where(c => c != ' ')
                .ToArray();
            
            foreach (var ch in chars)
            {
                if (!char.IsLetter(ch))
                {
                    SendValidationError($"{inputPart} contains non-alphabetic characters");
                    return false;
                }
            }

            return NextValidator?.Validate(input, inputPart) ?? true;
        }
    }
}