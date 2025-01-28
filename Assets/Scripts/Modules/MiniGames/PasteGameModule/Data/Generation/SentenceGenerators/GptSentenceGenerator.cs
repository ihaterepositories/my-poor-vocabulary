using System.Threading.Tasks;
using Modules.MiniGames.PasteGameModule.Data.Generation.SentenceGenerators.Interfaces;

namespace Modules.MiniGames.PasteGameModule.Data.Generation.SentenceGenerators
{
    public class GptSentenceGenerator : IAsyncSentenceGenerator
    {
        public Task<string> GenerateSentenceWithWord(string word)
        {
            throw new System.NotImplementedException();
        }
    }
}