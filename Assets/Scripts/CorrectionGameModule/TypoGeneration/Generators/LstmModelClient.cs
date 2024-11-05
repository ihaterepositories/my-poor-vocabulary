using System.Text;
using System.Threading.Tasks;
using Constants;
using CorrectionGameModule.TypoGeneration.Interfaces;
using UnityEngine;
using UnityEngine.Networking;

namespace CorrectionGameModule.TypoGeneration.Generators
{
    public class LstmModelClient : IAsyncTypoGenerator
    {
        private readonly string _apiUrl = AppConstants.LstmModelApiUrl;
        
        public async Task<string>GenerateTypo(string word)
        {
            var jsonData = $"{{\"word\": \"{word}\"}}";
            var postData = Encoding.UTF8.GetBytes(jsonData);
            
            using var request = new UnityWebRequest(_apiUrl, "POST");
            request.uploadHandler = new UploadHandlerRaw(postData);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            
            var operation = request.SendWebRequest();
            while (!operation.isDone) await Task.Yield();
            
            if (request.result == UnityWebRequest.Result.ConnectionError || 
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"LstmModelClient: {request.error}");
                return null;
            }
            
            return request.downloadHandler.text.Trim('[', ']', '"');
        }
    }
}