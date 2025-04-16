using Modules.SchoolSystem.AccountRegistration.Data;
using Modules.SchoolSystem.AccountRegistration.Requests;
using Modules.SchoolSystem.Enums;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Functional;
using Utils.ResponseModels;
using Utils.ResponseModels.Enums;

namespace Modules.SchoolSystem.AccountRegistration
{
    public class AccountRegistrar : MonoBehaviour
    {
        [SerializeField] private AccountType accountType;
        [SerializeField] private Button submitButton;
        [SerializeField] private ActionResultMessageView messageText;
        [SerializeField] private RegisterAccountForm form;
        
        private RegistrationRequestHandler _requestHandler;

        private void Start()
        {
            _requestHandler = new RegistrationRequestHandler();
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

            StartCoroutine(_requestHandler.SendRegisterRequest(accountType, form.ToJson(), ShowRequestResultMessage));
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