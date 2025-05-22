using System;
using System.Collections.Generic;
using Constants;
using UnityEngine;

namespace Modules.PersonalVocabulary.Data.Models
{
    public class Word
    {
        public string Original { get; private set; }
        public List<string> Translations { get; private set; }
        public DateTime AddingDate { get; private set; }
        
        /// <summary>
        /// Ranges from 0 to infinity, where 0 means no problems with the word.
        /// </summary>
        public int ProblematicCoefficient { get; set; }
        
        public Word(string original, List<string> translations)
        {
            Original = original;
            Translations = translations;
            AddingDate = DateTime.Now;
        }

        public bool CheckForNewTranslationAddAbility(string translation)
        {
            return Translations.Count < AppConstants.MaxTranslationsPerWord && !Translations.Contains(translation);
        }
        
        public void AddTranslation(string translation)
        {
            if (!string.IsNullOrWhiteSpace(translation) && 
                !Translations.Contains(translation) && 
                Translations.Count < AppConstants.MaxTranslationsPerWord)
            {
                Translations.Add(translation);
            }
            else
            {
                Debug.LogError($"Can`t add translation: {translation}");
            }
        }
    }
}