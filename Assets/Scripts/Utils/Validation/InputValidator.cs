using System.Collections.Generic;
using Utils.Validation.Validators;
using Utils.Validation.Validators.Abstraction;

namespace Utils.Validation
{
    // Chain of Responsibility pattern for original word input validating
    // *next validator will be executed if previous did not catch error* 
    public class InputValidator
    {
        public string LastValidationErrorDescription { get; private set; }
        
        public InputValidator()
        {
            InputValidationChainMember.OnValidationFailed += SetValidationError;
        }
        
        private void SetValidationError(string message)
        {
            LastValidationErrorDescription = message;
        }
        
        public bool Validate(string input)
        {
            var validatorConfig = new ValidatorConfig();
            validatorConfig.UseAll();
            
            var validationChain = CreateChain(validatorConfig);
            return validationChain.Validate(input);
        }
        
        public bool Validate(string input, ValidatorConfig validatorConfig)
        {
            var validationChain = CreateChain(validatorConfig);
            return validationChain.Validate(input);
        }
        
        public bool ValidateForMany(List<string> inputs, ValidatorConfig config)
        {
            var validationChain = CreateChain(config);

            foreach (var input in inputs)
            {
                if(!validationChain.Validate(input)) return false;
            }
            
            return true;
        }
        
        private InputValidationChainMember CreateChain(ValidatorConfig config)
        {
            InputValidationChainMember first = null;
            InputValidationChainMember current = null;
        
            void AddValidator(InputValidationChainMember validator)
            {
                if (first == null)
                {
                    first = validator;
                    current = validator;
                }
                else
                {
                    current!.SetNext(validator);
                    current = validator;
                }
            }
        
            if (config.UseEmptyValidator) AddValidator(new EmptyInputValidator());
            if (config.UseVocabularyWordLengthValidator) AddValidator(new VocabularyWordLengthValidator());
            if (config.UseExtraSpacesValidator) AddValidator(new ExtraSpacesValidator());
            if (config.UseAlphabeticWordValidator) AddValidator(new AlphabeticWordValidator());
            if (config.UseVocabularyWordWordsCountValidator) AddValidator(new VocabularyWordWordsCountValidator());
        
            return first;
        }
    }
}