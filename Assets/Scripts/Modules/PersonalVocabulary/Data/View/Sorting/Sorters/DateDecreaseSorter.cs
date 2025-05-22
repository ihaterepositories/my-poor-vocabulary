using System.Collections.Generic;
using System.Linq;
using Modules.PersonalVocabulary.Data.Models;
using Modules.PersonalVocabulary.Data.View.Sorting.Interfaces;

namespace Modules.PersonalVocabulary.Data.View.Sorting.Sorters
{
    public class DateDecreaseSorter : IVocabularySorter
    {
        public List<Word> Sort(List<Word> vocabulary)
        {
            return vocabulary.OrderByDescending(w => w.AddingDate).ToList();
        }
    }
}