using System.Collections.Generic;
using VocabularyModule.Data.Models;

namespace VocabularyModule.Data.View.Sorting.Interfaces
{
    public interface IVocabularySorter
    {
        public List<Word> Sort(List<Word> vocabulary);
    }
}