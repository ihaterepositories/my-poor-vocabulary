using System.Collections.Generic;
using Modules.SchoolSystem.View.DataModels.Student;

namespace Modules.SchoolSystem.View.Sorting.Interfaces
{
    public interface ITeacherAccountStudentsSorter
    {
        public List<StudentCredentials> Sort(List<StudentCredentials> students);
    }
}