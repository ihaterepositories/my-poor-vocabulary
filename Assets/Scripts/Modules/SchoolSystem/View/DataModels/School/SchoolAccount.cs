using System;
using System.Collections.Generic;
using Modules.SchoolSystem.View.DataModels.Interfaces;
using Modules.SchoolSystem.View.DataModels.Student;
using Modules.SchoolSystem.View.DataModels.Teacher;

namespace Modules.SchoolSystem.View.DataModels.School
{
    public class SchoolAccount : ISchoolSystemAccount
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public string KeyForEnrollment { get; set; } = null!;

        public List<TeacherCredentials> TeachersCredentials { get; set; } = new();
        public List<StudentCredentials> StudentsCredentials { get; set; } = new();
        
        public void LogFieldsToConsole()
        {
            throw new System.NotImplementedException();
        }
    }
}