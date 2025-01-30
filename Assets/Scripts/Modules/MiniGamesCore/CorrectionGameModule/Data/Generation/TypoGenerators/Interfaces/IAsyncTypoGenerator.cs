using System.Threading.Tasks;

namespace Modules.MiniGamesCore.CorrectionGameModule.Data.Generation.TypoGenerators.Interfaces
{
    public interface IAsyncTypoGenerator
    {
        public Task<string> GenerateTypo(string word);
    }
}