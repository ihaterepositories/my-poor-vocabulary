using System.Threading.Tasks;

namespace Modules.MiniGames.CorrectionGameModule.TypoGeneration.Interfaces
{
    public interface IAsyncTypoGenerator
    {
        public Task<string> GenerateTypo(string word);
    }
}