using VocabularyModule.Data.View.Sorting.Interfaces;

namespace VocabularyModule.Data.View.Sorting
{
    public static class VocabularySortFactory
    {
        public static IVocabularySorter GetSorter(int sortType)
        {
            return sortType switch
            {
                0 => new Sorters.DateDecreaseSorter() // 0: newest
                ,
                1 => new Sorters.DateIncreaseSorter() // 1: oldest
                ,
                2 => new Sorters.AlphabetIncreaseSorter() // 2: a-z
                ,
                3 => new Sorters.AlphabetDecreaseSorter() // 3: z-a
                ,
                _ => new Sorters.DateDecreaseSorter()
            };
        }
    }
}