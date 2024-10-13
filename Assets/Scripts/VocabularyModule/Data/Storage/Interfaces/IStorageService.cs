using VocabularyModule.Data.Models;

namespace VocabularyModule.Data.Storage.Interfaces
{
    public interface IStorageService
    {
        public void Save(Vocabulary vocabulary);
        public Vocabulary Load();
    }
}