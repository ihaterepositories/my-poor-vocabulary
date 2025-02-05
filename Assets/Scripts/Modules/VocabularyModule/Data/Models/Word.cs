using System;
using System.Collections.Generic;

namespace Modules.VocabularyModule.Data.Models
{
    public class Word
    {
        public string Original { get; set; }
        public List<string> Translations { get; set; }
        public DateTime AddingDate { get; set; }
        public bool IsIncorrectTranslatedInTranslationGame { get; set; }
        
        public Word(string original, string translation)
        {
            Original = original;
            Translations = new List<string>{translation};
            AddingDate = DateTime.Now;
            IsIncorrectTranslatedInTranslationGame = false;
        }
    }
}