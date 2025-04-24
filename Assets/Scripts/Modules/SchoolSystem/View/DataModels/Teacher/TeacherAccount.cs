using System;
using System.Collections.Generic;
using Modules.SchoolSystem.View.DataModels.Interfaces;
using Modules.SchoolSystem.View.DataModels.School;
using Modules.SchoolSystem.View.DataModels.Student;

namespace Modules.SchoolSystem.View.DataModels.Teacher
{
    public class TeacherAccount : ISchoolSystemAccount
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public string KeyForEnrollment { get; set; } = string.Empty;
        public SchoolCredentials SchoolCredentials { get; set; } = new();
        public List<StudentCredentials> StudentsCredentials { get; set; } = new();
        
        public void LogFieldsToConsole()
        {
            throw new System.NotImplementedException();
        }
    }
}