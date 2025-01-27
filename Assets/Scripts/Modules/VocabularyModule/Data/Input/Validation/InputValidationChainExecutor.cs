using Modules.VocabularyModule.Data.Input.Validation.Validators;
using Modules.VocabularyModule.Data.Input.Validation.Validators.Interfaces;

namespace Modules.VocabularyModule.Data.Input.Validation
{
    // Chain of Responsibility pattern for original word input validating
    // *next validator will be executed if previous did not catch error* 
    public class InputValidationChainExecutor
    {
        public string LastValidationError { get; private set; }
        
        public InputValidationChainExecutor()
        {
            InputValidator.OnValidationFailed += SetValidationError;
        }
        
        private void SetValidationError(string message)
        {
            LastValidationError = message;
        }
        
        public bool Execute(string input, string inputPart)
        {
            var validationChain = CreateChain();
            return validationChain.Validate(input, inputPart);
        }
        
        private InputValidator CreateChain()
        {
            InputValidator emptyInputValidator = new EmptyInputValidator();
            InputValidator lengthValidator = new LengthValidator();
            InputValidator extraSpacesValidator = new ExtraSpacesValidator();
            InputValidator alphabeticWordValidator = new AlphabeticWordValidator();
            InputValidator wordsCountValidator = new WordsCountValidator();
            
            emptyInputValidator.SetNext(lengthValidator);
            lengthValidator.SetNext(extraSpacesValidator);
            extraSpacesValidator.SetNext(alphabeticWordValidator);
            alphabeticWordValidator.SetNext(wordsCountValidator);
            
            return emptyInputValidator;
        }
    }
}