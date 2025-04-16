using System.Text;
using System.Threading.Tasks;
using Constants;
using Modules.MiniGamesCore.PasteGameModule.Data.Generation.SentenceGenerators.DeserializingModels;
using Modules.MiniGamesCore.PasteGameModule.Data.Generation.SentenceGenerators.Interfaces;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Modules.MiniGamesCore.PasteGameModule.Data.Generation.SentenceGenerators
{
    public class GptSentenceGenerator : IAsyncSentenceGenerator
    {
        private readonly string _apiKey = AppKeys.OpenAiApiKey;
        private readonly string _apiUrl = AppWays.OpenAiApiUrl;
        
        public async Task<string> GenerateSentence(string word, string characterDescription)
        {
            return await SendRequestToGpt(GetPrompt(word, characterDescription));
        }

        private string GetPrompt(string word, string characterDescription)
        {
            return $"You are a character with the following traits: {characterDescription} " +
                   $"Write a natural sentence in the style of this character that includes the word \"{word}\". " +
                   $"Do not modify the form of the word. " +
                   $"Your response should be only the generated sentence with no explanations or additional text.";
        }

        private async Task<string> SendRequestToGpt(string prompt)
        {
            var requestData = new
            {
                model = "gpt-3.5-turbo",
                messages = new object[] { new { role = "user", content = prompt } },
                max_tokens = 50
            };

            string jsonData = JsonConvert.SerializeObject(requestData);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

            using UnityWebRequest request = new UnityWebRequest(_apiUrl, "POST");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + _apiKey);

            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                var responseJson = request.downloadHandler.text;
                var response = JsonConvert.DeserializeObject<GptResponse>(responseJson);
                
                return response.Choices[0].Message.Content;
            }
            else
            {
                Debug.LogError("Gpt prompt error: " + request.error);
                return null;
            }
        }
    }
}