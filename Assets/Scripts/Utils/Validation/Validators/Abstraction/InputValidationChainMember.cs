using System;

namespace Utils.Validation.Validators.Abstraction
{
    public abstract class InputValidationChainMember
    {
        protected InputValidationChainMember NextValidationChainMember;
        public static Action<string> OnValidationFailed;

        public void SetNext(InputValidationChainMember next)
        {
            NextValidationChainMember = next;
        }

        protected void SendValidationError(string message)
        {
            OnValidationFailed?.Invoke(message);
        }

        public abstract bool Validate(string input);
    }
}