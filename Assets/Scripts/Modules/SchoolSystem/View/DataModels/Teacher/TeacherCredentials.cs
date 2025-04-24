using System;

namespace Modules.SchoolSystem.View.DataModels.Teacher
{
    public class TeacherCredentials
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public string KeyForEnrollment { get; set; } = string.Empty;

        public int GetAge()
        {
            return DateTime.Now.Year - DateOfBirth.Year;
        }
    }
}