using System.Collections.Generic;
using Modules.PersonalVocabulary.Data.Models;

namespace Modules.PersonalVocabulary.Data.View.Sorting.Interfaces
{
    public interface IVocabularySorter
    {
        public List<Word> Sort(List<Word> vocabulary);
    }
}