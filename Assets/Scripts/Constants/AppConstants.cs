using System.IO;
using UnityEngine;

namespace Constants
{
    public static class AppConstants
    {
        public const int MaxTipLength = 25;
        public const int MaxWordLength = 45;
        
        public const float SceneLoadDelay = .3f;
        
        public const int LevelSplitExpCount = 1000;
        public const int ExpPerTest = 10;
        public const int ExpPerAddedWord = 15;
        public const int ExpPerDayReward = 50;
        
        public static readonly string _localStoragePath = Path.Combine(Application.persistentDataPath, "Vocabulary.json");
        public const string TypoGeneratingRequestUrl = "http://127.0.0.1:8000/predict_typo";
    }
}