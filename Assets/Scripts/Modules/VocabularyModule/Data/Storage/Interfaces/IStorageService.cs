using Modules.VocabularyModule.Data.Models;

namespace Modules.VocabularyModule.Data.Storage.Interfaces
{
    public interface IStorageService
    {
        public void Save(Vocabulary vocabulary);
        public Vocabulary Load();
    }
}