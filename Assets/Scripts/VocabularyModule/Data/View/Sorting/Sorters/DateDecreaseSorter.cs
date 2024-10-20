using System.Collections.Generic;
using System.Linq;
using VocabularyModule.Data.Models;
using VocabularyModule.Data.View.Sorting.Interfaces;

namespace VocabularyModule.Data.View.Sorting.Sorters
{
    public class DateDecreaseSorter : IVocabularySorter
    {
        public List<Word> Sort(List<Word> vocabulary)
        {
            return vocabulary.OrderByDescending(w => w.AddingDate).ToList();
        }
    }
}