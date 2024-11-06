using Modules.VocabularyModule.Data.Input;
using Modules.VocabularyModule.Data.Models;
using Modules.VocabularyModule.Data.Storage.Interfaces;
using UnityEngine;
using Zenject;

namespace Modules.VocabularyModule
{
    public class VocabularyController : MonoBehaviour
    {
        private IStorageService _storageService;

        public Vocabulary Vocabulary;
        
        [Inject]
        private void Construct(IStorageService storageService)
        {
            _storageService = storageService;
        }

        private void Awake()
        {
            LoadVocabularyFromStorage();
        }

        private void OnEnable()
        {
            WordAddController.OnWordAdded += SaveVocabularyToStorage;
        }
        
        private void OnDisable()
        {
            WordAddController.OnWordAdded -= SaveVocabularyToStorage;
        }

        private void LoadVocabularyFromStorage()
        {
            Vocabulary = _storageService.Load();
            Debug.Log("Loaded " + Vocabulary.GetCount() + " words");
        }
        
        private void SaveVocabularyToStorage()
        {
            _storageService.Save(Vocabulary);
        }
    }
}