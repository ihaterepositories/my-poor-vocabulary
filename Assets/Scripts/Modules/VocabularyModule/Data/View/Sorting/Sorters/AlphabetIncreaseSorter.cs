using System.Collections.Generic;
using System.Linq;
using Modules.VocabularyModule.Data.Models;
using Modules.VocabularyModule.Data.View.Sorting.Interfaces;

namespace Modules.VocabularyModule.Data.View.Sorting.Sorters
{
    public class AlphabetIncreaseSorter : IVocabularySorter
    {
        public List<Word> Sort(List<Word> vocabulary)
        {
            return vocabulary.OrderBy(w => w.Original).ToList();
        }
    }
}