using System;
using Modules.SchoolSystem.View.DataModels.Interfaces;
using Modules.SchoolSystem.View.DataModels.School;
using Modules.SchoolSystem.View.DataModels.Teacher;
using UnityEngine;

namespace Modules.SchoolSystem.View.DataModels.Student
{
    public class StudentAccount : ISchoolSystemAccount
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
    
        public DateTime LastVisitation { get; set; }
    
        public int GamesCompleted { get; set; }
        public float AverageScorePerGame { get; set; }
    
        public int WordsCountInVocabulary { get; set; }
        public DateTime LastVocabularyUpdate { get; set; }
    
        public SchoolCredentials SchoolCredentials { get; set; }
        public TeacherCredentials TeacherCredentials { get; set; }
        
        public void LogFieldsToConsole()
        {
            Debug.Log(SchoolCredentials.Name);
            Debug.Log(TeacherCredentials.FirstName + " " + TeacherCredentials.LastName);
        }
    }
}