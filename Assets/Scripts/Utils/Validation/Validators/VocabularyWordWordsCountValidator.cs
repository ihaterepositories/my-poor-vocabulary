namespace Utils.Validation.Validators
{
    public class VocabularyWordWordsCountValidator : Abstraction.InputValidationChainMember
    {
        public override bool Validate(string input)
        {
            
            if (input.Split(' ').Length > 2)
            {
                SendValidationError($"{input} should contain no more than 2 words");
                return false;
            }
            
            return NextValidationChainMember?.Validate(input) ?? true;
        }
    }
}