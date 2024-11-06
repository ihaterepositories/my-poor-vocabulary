using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Random = System.Random;

namespace Modules.VocabularyModule.Data.Models
{
    public class Vocabulary
    {
        private List<Word> Words { get; set; }
        
        public Vocabulary(List<Word> words)
        {
            Words = words;
        }
        
        public List<Word> GetAll()
        {
            return Words;
        }

        public List<string> GetAllOriginals()
        {
            return Words
                .Select(w => w.Original)
                .ToList();
        }
        
        public List<string> GetAllTranslations()
        {
            return Words
                .Select(w => w.Translation)
                .ToList();
        }
        
        public Word GetRandom()
        {
            var random = new Random();
            return Words[random.Next(Words.Count)];
        }
        
        public List<Word> GetIncorrectTranslatedInTranslationGame()
        {
            return Words.FindAll(word => word.IsIncorrectTranslatedInTranslationGame);
        }

        public List<Word> GetSortedByNewest()
        {
            return Words.OrderByDescending(word => word.AddingDate).ToList();
        }

        public int GetCount()
        {
            return Words.Count;
        }
        
        public void Add(Word word)
        {
            Words.Add(word);
        }

        public void ModifyTranslationTestAttemptFor(string word, bool isCorrect)
        {
            var wordFromVocabulary = Words.FirstOrDefault(w => w.Original == word || w.Translation == word);
            if (wordFromVocabulary != null)
            {
                wordFromVocabulary.IsIncorrectTranslatedInTranslationGame = isCorrect;
            }
        }
    }
}