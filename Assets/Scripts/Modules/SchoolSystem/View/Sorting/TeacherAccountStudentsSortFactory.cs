using Modules.SchoolSystem.View.Sorting.Interfaces;

namespace Modules.SchoolSystem.View.Sorting
{
    public static class TeacherAccountStudentsSortFactory
    {
        public static ITeacherAccountStudentsSorter GetSorter(int sortType)
        {
            return sortType switch
            {
                0 => new Sorters.TeacherAccountStudentsByNameSorter() // 0: name
                ,
                1 => new Sorters.TeacherAccountStudentsByVocabularyCapacitySorter() // 1: vocabulary capacity
                ,
                2 => new Sorters.TeacherAccountStudentsByGamesCompletedSorter() // 2: games completed
                ,
                3 => new Sorters.TeacherAccountStudentsByAverageScorePerGameSorter() // 3: average score per game
                ,
                _ => new Sorters.TeacherAccountStudentsByNameSorter()
            };
        }
    }
}