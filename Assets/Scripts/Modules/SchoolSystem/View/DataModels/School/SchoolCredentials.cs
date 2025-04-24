using System;

namespace Modules.SchoolSystem.View.DataModels.School
{
    public class SchoolCredentials
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public string KeyForEnrollment { get; set; } = string.Empty;
    }
}