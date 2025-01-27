using System.Threading.Tasks;

namespace Modules.MiniGames.CorrectionGameModule.Data.Generation.Generation.TypoGenerators.Interfaces
{
    public interface IAsyncTypoGenerator
    {
        public Task<string> GenerateTypo(string word);
    }
}