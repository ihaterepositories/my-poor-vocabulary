using System;
using System.Collections;
using Constants;
using Modules.SchoolSystem.Enums;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Utils.ResponseModels;
using StatusCode = Utils.ResponseModels.Enums.StatusCode;

namespace Modules.SchoolSystem.Registration.Requests
{
    public static class RegistrationRequestHandler
    {
        public static IEnumerator SendRegisterRequest(AccountType accountType, string jsonBody, Action<NoReturnDataResponse> onResult)
        {
            string url = BuildUrl(accountType);

            using UnityWebRequest request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result is UnityWebRequest.Result.ConnectionError)
            {
                onResult?.Invoke(new NoReturnDataResponse
                {
                    StatusCode = StatusCode.InternalServerError,
                    ErrorDescription = "Cannot connect with a server..."
                });
            }
            else
            {
                try
                {
                    var response = JsonConvert.DeserializeObject<NoReturnDataResponse>(request.downloadHandler.text);
                    onResult?.Invoke(response);
                }
                catch (Exception ex)
                {
                    onResult?.Invoke(new NoReturnDataResponse
                    {
                        StatusCode = StatusCode.InternalServerError,
                        ErrorDescription = "Program error: " + ex.Message
                    });
                }
            }
        }

        private static string BuildUrl(AccountType accountType)
        {
            string accountTypeForWay;
            switch (accountType)
            {
                case AccountType.Student: accountTypeForWay = "students"; break;
                case AccountType.Teacher: accountTypeForWay = "teachers"; break;
                case AccountType.School: accountTypeForWay = "schools"; break;
                default: throw new Exception("Unknown accountType");
            }
            return AppWays.SchoolSystemApiHost + $"/{accountTypeForWay}/register";
        }
    }
}