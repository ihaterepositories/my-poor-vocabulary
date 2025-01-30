using System.Threading.Tasks;
using Modules.MiniGamesCore.PasteGameModule.Data.Generation.SentenceGenerators.Interfaces;

namespace Modules.MiniGamesCore.PasteGameModule.Data.Generation.SentenceGenerators
{
    public class TestSentenceGenerator : IAsyncSentenceGenerator
    {
        public Task<string> GenerateSentenceWithWord(string word)
        {
            return Task.FromResult("Test sentence with word: " + word);
        }
    }
}