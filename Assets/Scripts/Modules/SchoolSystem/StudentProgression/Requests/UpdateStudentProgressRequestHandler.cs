using System;
using System.Collections;
using System.Text;
using Constants;
using Modules.SchoolSystem.StudentProgression.Models;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Utils.ResponseModels;
using Utils.ResponseModels.Enums;

namespace Modules.SchoolSystem.StudentProgression.Requests
{
    public class UpdateStudentProgressRequestHandler
    {
        public static IEnumerator SendUpdateProgressRequest(UpdateStudentProgressModel progressModel, System.Action<NoReturnDataResponse> onComplete)
        {
            var jsonContent = JsonConvert.SerializeObject(progressModel);
            var request = new UnityWebRequest(AppWays.SchoolSystemApiHost + $"/students/updateProgress", "PUT")
            {
                uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonContent)),
                downloadHandler = new DownloadHandlerBuffer()
            };
            
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            try
            {
                if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    onComplete?.Invoke(new NoReturnDataResponse()
                    {
                        ErrorDescription = "Cannot connect with a server.",
                        StatusCode = StatusCode.InternalServerError
                    });
                }
                else
                {
                    var response = JsonConvert.DeserializeObject<NoReturnDataResponse>(request.downloadHandler.text);
                    onComplete?.Invoke(response);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                onComplete?.Invoke(new NoReturnDataResponse()
                {
                    ErrorDescription = "Program error.",
                    StatusCode = StatusCode.InternalServerError
                });
            }
        }
    }
}