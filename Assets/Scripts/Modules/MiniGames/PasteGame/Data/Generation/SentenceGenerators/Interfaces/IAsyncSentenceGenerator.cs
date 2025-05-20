using System.Threading.Tasks;

namespace Modules.MiniGames.PasteGame.Data.Generation.SentenceGenerators.Interfaces
{
    public interface IAsyncSentenceGenerator
    {
        public Task<string> GenerateSentence(string word, string characterDescription);
    }
}