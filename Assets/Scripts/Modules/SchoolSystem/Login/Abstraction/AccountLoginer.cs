using Modules.SchoolSystem.Login.DataModels;
using Modules.SchoolSystem.Login.Requests;
using Modules.SchoolSystem.Login.SerializingModels;
using Modules.SchoolSystem.View.Abstraction;
using Modules.SchoolSystem.View.DataModels.Interfaces;
using UnityEngine;
using UserInterface.Functional;
using Utils.ResponseModels;
using Utils.ResponseModels.Enums;

namespace Modules.SchoolSystem.Login.Abstraction
{
    public abstract class AccountLoginer<T> : MonoBehaviour where T : ISchoolSystemAccount
    {
        [SerializeField] private ActionResultMessageView loginErrorView;
        [SerializeField] private RectTransform loginFormsParentRect;
        [SerializeField] private LoginAccountForm loginAccountForm;
        [SerializeField] private AccountView<T> accountView;

        private void Start()
        {
            loginAccountForm.signInButton.onClick.AddListener(BuildRequest);
        }

        private void BuildRequest()
        {
            loginErrorView.ShowProgress("Processing...");
            
            if (!loginAccountForm.IsFieldsValid())
            {
                loginErrorView.ShowError(loginAccountForm.ValidationErrorDescription);
                return;
            }

            var loginData = new LoginData {Email = loginAccountForm.emailField.text, Password = loginAccountForm.passwordField.text};
            StartCoroutine(GetAccountRequestHandler.SendGetAccountRequest<T>(loginData, TryShowAccountForm));
        }
        
        private void TryShowAccountForm(BaseResponse<T> response)
        {
            if (response.StatusCode != StatusCode.Ok)
            {
                loginErrorView.ShowError(response.ErrorDescription);
                return;
            }

            loginFormsParentRect.gameObject.SetActive(false);
            accountView.Show(response.Data);
        }
    }
}