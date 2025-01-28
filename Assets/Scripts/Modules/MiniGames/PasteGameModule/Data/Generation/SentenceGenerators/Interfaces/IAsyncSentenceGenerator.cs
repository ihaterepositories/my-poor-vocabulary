using System.Threading.Tasks;

namespace Modules.MiniGames.PasteGameModule.Data.Generation.SentenceGenerators.Interfaces
{
    public interface IAsyncSentenceGenerator
    {
        public Task<string> GenerateSentenceWithWord(string word);
    }
}