using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

namespace Modules.PersonalVocabulary.Data.Models
{
    // TODO: refactor (add some methods to Word model)
    public class Vocabulary
    {
        private List<Word> Words { get; }
        private int _lastRandomWordListIndex = int.MinValue;
        
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
            var index = random.Next(Words.Count);
            
            if (index == _lastRandomWordListIndex)
            {
                return GetRandom();
            }

            _lastRandomWordListIndex = index;
            return Words[index];
        }
        
        public List<Word> GetProblematicWords()
        {
            return Words.FindAll(word => word.ProblematicCoefficient >= 2);
        }

        public List<Word> GetSortedByNewest()
        {
            return Words.OrderByDescending(word => word.AddingDate).ToList();
        }

        public int GetWordsCount()
        {
            return Words.Count;
        }
        
        public void Add(Word word)
        {
            Words.Add(word);
        }

        public void ModifyProblematicStatusFor(string word, bool isSuccessfullyTranslated)
        {
            var wordFromVocabulary = Words.FirstOrDefault(w => w.Original == word || w.Translations.Any(t => t == word));
            if (wordFromVocabulary != null)
            {
                if (isSuccessfullyTranslated)
                {
                    if (wordFromVocabulary.ProblematicCoefficient > 0)
                    {
                        wordFromVocabulary.ProblematicCoefficient -= 1;
                    }
                }
                else
                {
                    wordFromVocabulary.ProblematicCoefficient += 1;
                }
            }
        }
        
        public bool CheckIfExistsByOriginalWord(string originalWord)
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