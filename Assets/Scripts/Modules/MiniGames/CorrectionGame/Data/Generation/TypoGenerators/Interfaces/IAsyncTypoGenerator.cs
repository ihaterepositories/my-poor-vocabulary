using System.Threading.Tasks;

namespace Modules.MiniGames.CorrectionGame.Data.Generation.TypoGenerators.Interfaces
{
    public interface IAsyncTypoGenerator
    {
        public Task<string> GenerateTypo(string word);
    }
}