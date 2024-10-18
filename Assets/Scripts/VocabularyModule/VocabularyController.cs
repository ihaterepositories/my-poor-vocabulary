using UnityEngine;
using VocabularyModule.Data.Input;
using VocabularyModule.Data.Models;
using VocabularyModule.Data.Storage.Interfaces;
using Zenject;

namespace VocabularyModule
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
            Debug.Log("Words loaded: " + Vocabulary.Words.Count);
        }
        
        private void SaveVocabularyToStorage()
        {
            _storageService.Save(Vocabulary);
        }
    }
}