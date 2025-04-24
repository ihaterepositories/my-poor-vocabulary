using System.Collections.Generic;
using System.Linq;
using Modules.SchoolSystem.View.DataModels.Student;
using Modules.SchoolSystem.View.Sorting.Interfaces;

namespace Modules.SchoolSystem.View.Sorting.Sorters
{
    public class TeacherAccountStudentsByGamesCompletedSorter : ITeacherAccountStudentsSorter
    {
        public List<StudentCredentials> Sort(List<StudentCredentials> students)
        {
            return students.OrderByDescending(s => s.GamesCompleted).ToList();
        }
    }
}