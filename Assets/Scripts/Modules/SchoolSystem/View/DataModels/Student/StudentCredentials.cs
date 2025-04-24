using System;

namespace Modules.SchoolSystem.View.DataModels.Student
{
    public class StudentCredentials
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastVisitation { get; set; }
        public int GamesCompleted { get; set; }
        public float AverageScorePerGame { get; set; }
        public int WordsCountInVocabulary { get; set; }
        public DateTime LastVocabularyUpdate { get; set; }

        public int GetAge()
        {
            return DateTime.Now.Year - DateOfBirth.Year;
        }
    }
}