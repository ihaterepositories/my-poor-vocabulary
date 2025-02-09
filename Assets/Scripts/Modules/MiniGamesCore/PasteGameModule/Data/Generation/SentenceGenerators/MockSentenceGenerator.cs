using System.Threading.Tasks;
using Modules.MiniGamesCore.PasteGameModule.Data.Generation.SentenceGenerators.Interfaces;
using UnityEngine;

namespace Modules.MiniGamesCore.PasteGameModule.Data.Generation.SentenceGenerators
{
    public class MockSentenceGenerator : IAsyncSentenceGenerator
    {
        public async Task<string> GenerateSentence(string word, string characterDescription)
        {
            await Task.Delay(50);
            return $"Test sentence with word: {word}, and character description: {characterDescription}";
        }
    }
}