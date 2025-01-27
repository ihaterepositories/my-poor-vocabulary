using Modules.VocabularyModule.Data.Input.Validation.Validators.Interfaces;

namespace Modules.VocabularyModule.Data.Input.Validation.Validators
{
    public class WordsCountValidator : InputValidator
    {
        public override bool Validate(string input, string inputPart)
        {
            
            if (input.Split(' ').Length > 2)
            {
                SendValidationError($"{inputPart} should contain no more than 2 words");
                return false;
            }
            
            return NextValidator?.Validate(input, inputPart) ?? true;
        }
    }
}