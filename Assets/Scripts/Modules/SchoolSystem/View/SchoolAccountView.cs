using Modules.SchoolSystem.View.Abstraction;
using Modules.SchoolSystem.View.DataModels.School;
using Modules.SchoolSystem.View.SerializingModels;
using UnityEngine;
using Utils.Validation;

namespace Modules.SchoolSystem.View
{
    public class SchoolAccountView : AccountView<SchoolAccount>
    {
        [SerializeField] private SchoolAccountViewUiFields uiFields;
        
        private SchoolAccount _schoolAccount;
        private InputValidator _inputValidator;

        private void Start()
        {
            _inputValidator = new InputValidator();
            uiFields.searchTeacherInputField.onValueChanged.AddListener(ShowTeachersWithNameContains);
            uiFields.searchStudentInputField.onValueChanged.AddListener(ShowStudentsWithNameContains);
        }

        protected override void FillUiFields(SchoolAccount account)
        {
            _schoolAccount = account;
            uiFields.personalInfoText.text = 
                account.Name + "\n" + 
                account.Address + "\n" + 
                account.Email + ", " + account.PhoneNumber;
            
            uiFields.registrationDateText.text = $"Registration date: {account.RegistrationDate.ToShortDateString()}";
            
            uiFields.enrollmentKeyText.text = $"KEY: {account.KeyForEnrollment}";
            
            ShowTeachers();
            ShowStudents();
        }
        
        private void ShowTeachersWithNameContains(string searchName)
        {
            if (!_inputValidator.Validate(searchName, ValidatorConfig.DefaultInputFieldValidationConfig))
            {
                ShowTeachers();
                return;
            }
            
            string teachers = "\n";

            foreach (var t in _schoolAccount.TeachersCredentials)
            {
                string fullName = t.FirstName + " " + t.LastName;
                if (fullName.ToLower().Contains(searchName.ToLower()))
                {
                    teachers += 
                        fullName + " (" + t.Email + ")" + "\n" +
                        t.GetAge() + " years old" + "\n" +
                        "Registered in " + t.RegistrationDate.ToShortDateString() +
                        "\n\n";
                }
            }
            
            uiFields.teachersListingText.text = teachers + "\n";
        }

        private void ShowStudentsWithNameContains(string searchName)
        {
            if (!_inputValidator.Validate(searchName, ValidatorConfig.DefaultInputFieldValidationConfig))
            {
                ShowStudents();
                return;
            }
            
            string students = "\n";

            foreach (var s in _schoolAccount.StudentsCredentials)
            {
                string fullName = s.FirstName + " " + s.LastName;
                if (fullName.ToLower().Contains(searchName.ToLower()))
                {
                    students +=
                        fullName + " (" + s.Email + ")" + "\n" +
                        s.GetAge() + " years old" + "\n" +
                        "Registered in " + s.RegistrationDate.ToShortDateString() +
                        "\n\n";
                }
            }
            
            uiFields.studentsListingText.text = students + "\n";
        }

        private void ShowTeachers()
        {
            string teachers = "\n";

            foreach (var t in _schoolAccount.TeachersCredentials)
            {
                string fullName = t.FirstName + " " + t.LastName;
                teachers += 
                    fullName + " (" + t.Email + ")" + "\n" +
                    t.GetAge() + " years old" + "\n" +
                    "Registered in " + t.RegistrationDate.ToShortDateString() +
                    "\n\n";
            }
            
            uiFields.teachersListingText.text = teachers + "\n";
        }

        private void ShowStudents()
        {
            string students = "\n";

            foreach (var s in _schoolAccount.StudentsCredentials)
            {
                string fullName = s.FirstName + " " + s.LastName;
                students +=
                    fullName + " (" + s.Email + ")" + "\n" +
                    s.GetAge() + " years old" + "\n" +
                    "Registered in " + s.RegistrationDate.ToShortDateString() +
                    "\n\n";
            }
            
            uiFields.studentsListingText.text = students + "\n";
        }
    }
}