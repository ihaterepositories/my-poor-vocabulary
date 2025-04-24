using System;
using System.Collections;
using System.Collections.Generic;
using Constants;
using Modules.SchoolSystem.Login.DataModels;
using Modules.SchoolSystem.View.DataModels.School;
using Modules.SchoolSystem.View.DataModels.Student;
using Modules.SchoolSystem.View.DataModels.Teacher;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Utils.ResponseModels;
using Utils.ResponseModels.Enums;

namespace Modules.SchoolSystem.Login.Requests
{
    public static class GetAccountRequestHandler
    {
        private static readonly Dictionary<Type, string> AccountTypePaths = new()
        {
            { typeof(StudentAccount), "students" },
            { typeof(TeacherAccount), "teachers" },
            { typeof(SchoolAccount), "schools" }
        };
        
        public static IEnumerator SendGetAccountRequest<T>(LoginData loginData, Action<BaseResponse<T>> onResult)
        {
            string url = BuildUrl(typeof(T), loginData.Email, loginData.Password);

            using UnityWebRequest request = new UnityWebRequest(url, "GET");
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            
            yield return request.SendWebRequest();
            
            if (request.result is UnityWebRequest.Result.ConnectionError)
            {
                onResult?.Invoke(new BaseResponse<T>
                {
                    StatusCode = StatusCode.InternalServerError,
                    ErrorDescription = "Cannot connect with a server...",
                    Data = default
                });
            }
            else
            {
                try
                {
                    var response = JsonConvert.DeserializeObject<BaseResponse<T>>(request.downloadHandler.text);
                    onResult?.Invoke(response);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    onResult?.Invoke(new BaseResponse<T>
                    {
                        StatusCode = StatusCode.InternalServerError,
                        ErrorDescription = "Program error...",
                        Data = default
                    });
                }
            }
        }

        private static string BuildUrl(Type accountType, string email, string password)
        {
            if (!AccountTypePaths.TryGetValue(accountType, out var accountTypeForWay))
            {
                Debug.LogError($"Unknown account type: {accountType}");
                accountTypeForWay = AccountTypePaths[typeof(StudentAccount)];
            }

            return AppWays.SchoolSystemApiHost + $"/{accountTypeForWay}/getFullAccountData/{email}/{password}";
        }
    }
}