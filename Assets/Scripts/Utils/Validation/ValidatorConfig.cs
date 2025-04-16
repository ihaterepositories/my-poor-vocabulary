namespace Utils.Validation.Validators
{
    public class ValidatorConfig
    {
        public bool UseEmptyValidator { get; set; } = false;
        public bool UseLengthValidator { get; set; } = false;
        public bool UseExtraSpacesValidator { get; set; } = false;
        public bool UseAlphabeticWordValidator { get; set; } = false;
        public bool UseWordsCountValidator { get; set; } = false;

        public void UseAll()
        {
            UseEmptyValidator = true;
            UseLengthValidator = true;
            UseExtraSpacesValidator = true;
            UseAlphabeticWordValidator = true;
            UseWordsCountValidator = true;
        }
    }
}