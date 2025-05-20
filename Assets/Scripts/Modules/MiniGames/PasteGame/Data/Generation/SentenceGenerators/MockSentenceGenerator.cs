using System.Threading.Tasks;
using Modules.MiniGames.PasteGame.Data.Generation.SentenceGenerators.Interfaces;

namespace Modules.MiniGames.PasteGame.Data.Generation.SentenceGenerators
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