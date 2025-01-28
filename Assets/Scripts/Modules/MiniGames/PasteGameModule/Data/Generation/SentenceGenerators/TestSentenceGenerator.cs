using System.Threading.Tasks;
using Modules.MiniGames.PasteGameModule.Data.Generation.SentenceGenerators.Interfaces;

namespace Modules.MiniGames.PasteGameModule.Data.Generation.SentenceGenerators
{
    public class TestSentenceGenerator : IAsyncSentenceGenerator
    {
        public Task<string> GenerateSentenceWithWord(string word)
        {
            return Task.FromResult("Test sentence with word: " + word);
        }
    }
}