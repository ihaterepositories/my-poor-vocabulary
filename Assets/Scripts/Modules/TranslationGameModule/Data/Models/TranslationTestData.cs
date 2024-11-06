using System.Collections.Generic;

namespace Modules.TranslationGameModule.Data.Models
{
    public class TranslationTestData
    {
        public string WordToTranslate { get; set; }
        public string CorrectAnswer { get; set; }
        public List<string> PossibleAnswers { get; set; }
        public bool IsWordToTranslateOriginal { get; set; }
    }
}