using System.Collections.Generic;
using System.Linq;
using Modules.SchoolSystem.View.DataModels.Student;
using Modules.SchoolSystem.View.Sorting.Interfaces;

namespace Modules.SchoolSystem.View.Sorting.Sorters
{
    public class TeacherAccountStudentsByNameSorter : ITeacherAccountStudentsSorter
    {
        public List<StudentCredentials> Sort(List<StudentCredentials> students)
        {
            return students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToList();
        }
    }
}