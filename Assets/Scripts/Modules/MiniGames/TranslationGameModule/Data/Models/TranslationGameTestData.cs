using System.Collections.Generic;

namespace Modules.MiniGames.TranslationGameModule.Data.Models
{
    public class TranslationGameTestData
    {
        public string WordToTranslate { get; set; }
        public string CorrectAnswer { get; set; }
        public List<string> PossibleAnswers { get; set; }
    }
}