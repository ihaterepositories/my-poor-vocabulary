using System;
using TMPro;
using UnityEngine.UI;

namespace Modules.SchoolSystem.View.SerializingModels
{
    [Serializable]
    public class SchoolAccountViewUiFields
    {
        public Text personalInfoText;
        public Text registrationDateText;
        public Text enrollmentKeyText;
        public TextMeshProUGUI teachersListingText;
        public TextMeshProUGUI studentsListingText;

        public InputField searchTeacherInputField;
        public InputField searchStudentInputField;
    }
}