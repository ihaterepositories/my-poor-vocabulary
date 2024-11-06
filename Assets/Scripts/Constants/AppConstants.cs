namespace Constants
{
    public class AppConstants
    {
        public const int MaxTipLength = 25;
        public const int MaxWordLength = 45;
        
        public const float SceneLoadDelay = .3f;
        
        public const string LocalStoragePath = "Assets/Resources/vocabulary.json";
        public const string TypoGeneratingRequestUrl = "http://127.0.0.1:8000/predict_typo";
    }
}