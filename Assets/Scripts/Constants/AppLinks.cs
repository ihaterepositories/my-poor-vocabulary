using System.IO;
using UnityEngine;

namespace Constants
{
    public static class AppLinks
    {
        public static readonly string LocalStoragePath = Path.Combine(Application.persistentDataPath, "Vocabulary.json");
        public const string LstmModelApiUrl = "http://127.0.0.1:8000/predict_typo";
        public const string OpenAiApiUrl = "https://api.openai.com/v1/chat/completions";
    }
}