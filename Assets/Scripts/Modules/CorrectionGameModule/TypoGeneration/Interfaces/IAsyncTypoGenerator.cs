using System.Threading.Tasks;

namespace Modules.CorrectionGameModule.TypoGeneration.Interfaces
{
    public interface IAsyncTypoGenerator
    {
        public Task<string> GenerateTypo(string word);
    }
}