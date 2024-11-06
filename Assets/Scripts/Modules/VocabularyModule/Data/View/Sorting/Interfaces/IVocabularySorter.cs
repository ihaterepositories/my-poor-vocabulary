using System.Collections.Generic;
using Modules.VocabularyModule.Data.Models;

namespace Modules.VocabularyModule.Data.View.Sorting.Interfaces
{
    public interface IVocabularySorter
    {
        public List<Word> Sort(List<Word> vocabulary);
    }
}