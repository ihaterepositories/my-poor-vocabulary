using Modules.SchoolSystem.Enums;
using Modules.SchoolSystem.Registration.Requests;
using Modules.SchoolSystem.Registration.SerializingModels;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Functional;
using Utils.ResponseModels;
using Utils.ResponseModels.Enums;

namespace Modules.SchoolSystem.Registration
{
    public class AccountRegistrar : MonoBehaviour
    {
        [SerializeField] private AccountType accountType;
        [SerializeField] private Button submitButton;
        [SerializeField] private ActionResultMessageView messageText;
        [SerializeField] private RegisterAccountForm form;

        private void Start()
        {
            submitButton.onClick.AddListener(BuildRequest);
        }
        
        private void BuildRequest()
        {
            messageText.ShowProgress("Processing...");
            
            if (!form.IsFieldsValid())
            {
                messageText.ShowError(form.ValidationErrorDescription);
                return;
            }

            StartCoroutine(RegistrationRequestHandler.SendRegisterRequest(accountType, form.ToJson(), ShowRequestResultMessage));
        }
        
        private void ShowRequestResultMessage(NoReturnDataResponse response)
        {
            if (response.StatusCode == StatusCode.Ok)
            {
                messageText.ShowSuccess("Registered successfully!", 1f);
            }
            else
            {
                messageText.ShowError(response.ErrorDescription);
            }
        }
    }
}