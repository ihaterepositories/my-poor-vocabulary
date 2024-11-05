using System.Threading.Tasks;

namespace CorrectionGameModule.TypoGeneration.Interfaces
{
    public interface ITypoGenerator
    {
        public Task<string> GenerateTypo(string word);
    }
}