namespace Modules.PersonalVocabulary.Data.Storage.Interfaces
{
    public interface IStorageService
    {
        public void Save(Models.Vocabulary vocabulary);
        public Models.Vocabulary Load();
    }
}