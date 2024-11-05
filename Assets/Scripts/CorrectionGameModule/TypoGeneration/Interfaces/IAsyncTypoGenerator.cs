using System.Threading.Tasks;

namespace CorrectionGameModule.TypoGeneration.Interfaces
{
    public interface IAsyncTypoGenerator
    {
        public Task<string> GenerateTypo(string word);
    }
}