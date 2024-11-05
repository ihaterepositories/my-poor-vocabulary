namespace Constants
{
    public class AppConstants
    {
        public const int MaxTipLength = 25;
        public const int MaxWordLength = 45;
        
        public const string LocalStorageFolder = "Assets/Resources/VocabularyLocalStorage";
        public const string LstmModelApiUrl = "http://127.0.0.1:8000/predict_typo";
    }
}