using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

namespace Modules.VocabularyModule.Data.Models
{
    // TODO: refactor (add some methods to Word model)
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
                .SelectMany(w => w.Translations)
                .ToList();
        }
        
        public Word GetByOriginal(string original)
        {
            return Words.FirstOrDefault(word => word.Original == original);
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
            var wordFromVocabulary = Words.FirstOrDefault(w => w.Original == word || w.Translations.Any(t => t == word));
            if (wordFromVocabulary != null)
            {
                wordFromVocabulary.IsIncorrectTranslatedInTranslationGame = isCorrect;
            }
        }
        
        public bool ContainsByOriginalWord(string originalWord)
        {
            return Words.Any(w => w.Original == originalWord);
        }

        public void DeleteWordByOriginal(string original)
        {
            var wordToRemove = Words.FirstOrDefault(w => w.Original == original);
            
            if (wordToRemove!=null)
            {
                Words.Remove(wordToRemove);
            }
        }
    }
}