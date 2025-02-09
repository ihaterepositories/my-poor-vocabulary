using System.Threading.Tasks;

namespace Modules.MiniGamesCore.PasteGameModule.Data.Generation.SentenceGenerators.Interfaces
{
    public interface IAsyncSentenceGenerator
    {
        public Task<string> GenerateSentence(string word, string characterDescription);
    }
}