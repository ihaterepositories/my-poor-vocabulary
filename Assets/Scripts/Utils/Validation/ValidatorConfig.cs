namespace Utils.Validation
{
    public class ValidatorConfig
    {
        public bool UseEmptyValidator { get; set; } = false;
        public bool UseVocabularyWordLengthValidator { get; set; } = false;
        public bool UseExtraSpacesValidator { get; set; } = false;
        public bool UseAlphabeticWordValidator { get; set; } = false;
        public bool UseVocabularyWordWordsCountValidator { get; set; } = false;

        public void UseAll()
        {
            UseEmptyValidator = true;
            UseVocabularyWordLengthValidator = true;
            UseExtraSpacesValidator = true;
            UseAlphabeticWordValidator = true;
            UseVocabularyWordWordsCountValidator = true;
        }

        public static ValidatorConfig DefaultInputFieldValidationConfig => new()
        {
            UseEmptyValidator = true,
            UseExtraSpacesValidator = true
        };
    }
}