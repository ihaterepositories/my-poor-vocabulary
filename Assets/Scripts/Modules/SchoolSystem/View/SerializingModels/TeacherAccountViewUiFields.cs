using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UserInterface.Functional;

namespace Modules.SchoolSystem.View.SerializingModels
{
    [Serializable]
    public class TeacherAccountViewUiFields
    {
        public Text personalInfoText;
        public Text registrationDateText;
        public Text enrollmentKeyText;
        public Text schoolInfoText;
        public TextMeshProUGUI studentsListingText;
        public InputField searchStudentInputField;
        public List<NumericDataTransferButton> sortTypeChooseButtons;
    }
}