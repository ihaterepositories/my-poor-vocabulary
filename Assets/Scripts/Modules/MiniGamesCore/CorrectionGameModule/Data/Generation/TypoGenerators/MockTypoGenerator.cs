using System.Threading.Tasks;
using Modules.MiniGamesCore.CorrectionGameModule.Data.Generation.TypoGenerators.Interfaces;

namespace Modules.MiniGamesCore.CorrectionGameModule.Data.Generation.TypoGenerators
{
    public class MockTypoGenerator : IAsyncTypoGenerator
    {
        public async Task<string> GenerateTypo(string word)
        {
            await Task.Delay(50);
            return $"Mock test with word: {word}";
        }
    }
}