using System;

namespace Modules.VocabularyModule.Data.Models
{
    public class Word
    {
        public string Original { get; set; }
        public string Translation { get; set; }
        public DateTime AddingDate { get; set; }
        public bool IsIncorrectTranslatedInTranslationGame { get; set; }
        
        public Word(string original, string translation)
        {
            Original = original;
            Translation = translation;
            AddingDate = DateTime.Now;
            IsIncorrectTranslatedInTranslationGame = false;
        }
    }
}