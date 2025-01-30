using System.Threading.Tasks;
using Modules.MiniGamesCore.PasteGameModule.Data.Generation.SentenceGenerators.Interfaces;

namespace Modules.MiniGamesCore.PasteGameModule.Data.Generation.SentenceGenerators
{
    public class GptSentenceGenerator : IAsyncSentenceGenerator
    {
        public Task<string> GenerateSentenceWithWord(string word)
        {
            throw new System.NotImplementedException();
        }
    }
}