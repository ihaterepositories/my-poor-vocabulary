using System.Globalization;
using Modules.SchoolSystem.View.Abstraction;
using Modules.SchoolSystem.View.DataModels.Student;
using Modules.SchoolSystem.View.SerializingModels;
using UnityEngine;

namespace Modules.SchoolSystem.View
{
    public class StudentAccountView : AccountView<StudentAccount>
    {
        [SerializeField] private StudentAccountViewUiFields uiFields;

        protected override void FillUiFields(StudentAccount account)
        {
            uiFields.personalInfoText.text = $"{account.FirstName} {account.LastName}" + "\n" + 
                                             $"({account.Email})";
            
            uiFields.registrationDateText.text = "* Registration date: " + account.RegistrationDate.ToShortDateString();
            
            uiFields.personalProgressText.text = 
                "Games completed: " + account.GamesCompleted + "\n" +
                "Average score per game: " + account.AverageScorePerGame.ToString(CultureInfo.InvariantCulture) + "\n" + 
                "Vocabulary capacity: " + account.WordsCountInVocabulary +  "\n" +
                "Last vocabulary update: " + account.LastVocabularyUpdate.ToShortDateString();
            
            uiFields.teacherInfoText.text = 
                $"Your teacher is {account.TeacherCredentials.FirstName} {account.TeacherCredentials.LastName} ({account.TeacherCredentials.GetAge()} y.o.)." + "\n" + 
                $"({account.TeacherCredentials.Email})";

            uiFields.schoolInfoText.text =
                $"Your school is {account.SchoolCredentials.Name}." + "\n" +
                $"Location: {account.SchoolCredentials.Address}." + "\n" +
                $"({account.SchoolCredentials.Email}, {account.SchoolCredentials.PhoneNumber})";
        }
    }
}