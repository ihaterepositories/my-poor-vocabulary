using System;

namespace Modules.VocabularyModule.Data.Input.Validation.Validators.Interfaces
{
    public abstract class InputValidator
    {
        protected InputValidator NextValidator;
        public static Action<string> OnValidationFailed;

        public void SetNext(InputValidator next)
        {
            NextValidator = next;
        }
        
        public void SendValidationError(string message)
        {
            OnValidationFailed?.Invoke(message);
        }

        public abstract bool Validate(string input, string inputPart);
    }
}