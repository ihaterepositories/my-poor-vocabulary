using System;
using System.Collections.Generic;
using Modules.SchoolSystem.View.Abstraction;
using Modules.SchoolSystem.View.DataModels.Student;
using Modules.SchoolSystem.View.DataModels.Teacher;
using Modules.SchoolSystem.View.SerializingModels;
using Modules.SchoolSystem.View.Sorting;
using UnityEngine;

namespace Modules.SchoolSystem.View
{
    public class TeacherAccountView : AccountView<TeacherAccount>
    {
        [SerializeField] private TeacherAccountViewUiFields uiFields;

        private List<StudentCredentials> _students;

        private void Start()
        {
            uiFields.searchStudentInputField.onValueChanged.AddListener(ShowSearchedStudents);
        }

        private void OnEnable()
        {
            foreach (var b in uiFields.sortTypeChooseButtons)
            {
                b.OnTransferData += ShowSortedStudentList;
            }
        }
        
        private void OnDisable()
        {
            foreach (var b in uiFields.sortTypeChooseButtons)
            {
                b.OnTransferData -= ShowSortedStudentList;
            }
        }

        protected override void FillUiFields(TeacherAccount account)
        {
            uiFields.personalInfoText.text =
                account.LastName + " " + account.FirstName + "\n" +
                $"({account.Email})";
            
            uiFields.registrationDateText.text = account.RegistrationDate.ToShortDateString();

            uiFields.enrollmentKeyText.text = "KEY: " + account.KeyForEnrollment;

            uiFields.schoolInfoText.text =
                account.SchoolCredentials.Name + "\n" + 
                $"({account.SchoolCredentials.Email})" + "\n" +
                $"Location: {account.SchoolCredentials.Address}" + "\n" +
                $"KEY: {account.SchoolCredentials.KeyForEnrollment}";

            _students = account.StudentsCredentials;
            ShowSortedStudentList(0);
        }

        private void ShowSortedStudentList(int sortType)
        {
            uiFields.searchStudentInputField.text = "";
            var sortedStuednts = GetSortedStudents(sortType);
            string formattedStudentsList = string.Empty;

            foreach (var s in sortedStuednts)
            {
                formattedStudentsList += GetFormattedStudentInfo(s);
            }
            
            uiFields.studentsListingText.text = formattedStudentsList + "\n";
        }
        
        private List<StudentCredentials> GetSortedStudents(int sortType)
        {
            return TeacherAccountStudentsSortFactory.GetSorter(sortType).Sort(_students);
        }

        private void ShowSearchedStudents(string searchName)
        {
            string formattedStudentsList = string.Empty;

            foreach (var s in _students)
            {
                var fullName = s.LastName + " " + s.FirstName;
                if (fullName.ToLower().Contains(searchName.ToLower()))
                {
                    formattedStudentsList += GetFormattedStudentInfo(s);
                }
            }
            
            uiFields.studentsListingText.text = formattedStudentsList + "\n";
        }

        private string GetFormattedStudentInfo(StudentCredentials s)
        {
            return 
                "\n" +
                s.LastName + " " + s.FirstName + " (" + s.Email + ")" + "\n" +
                s.GetAge() + " years old" + "\n" +
                "Registered in " + s.RegistrationDate.ToShortDateString() + "\n" +
                "Vocabulary capacity: " + s.WordsCountInVocabulary + "\n" +
                "Games completed: " + s.GamesCompleted + "\n" +
                "Average score per game: " + s.AverageScorePerGame + "\n";
        }
    }
}