using System;
using System.Collections.Generic;
using Constants;
using UnityEngine;

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

        public bool CheckForNewTranslationAddAbility(string translation)
        {
            return Translations.Count < AppConstants.MaxTranslationsPerWord && !Translations.Contains(translation);
        }
        
        public void AddTranslation(string translation)
        {
            Translations.Add(translation);
        }
    }
}