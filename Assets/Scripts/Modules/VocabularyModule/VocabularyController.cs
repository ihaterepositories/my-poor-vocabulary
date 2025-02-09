using System;
using Modules.VocabularyModule.Data.Delete;
using Modules.VocabularyModule.Data.Input;
using Modules.VocabularyModule.Data.Models;
using Modules.VocabularyModule.Data.Storage.Interfaces;
using UnityEngine;
using Zenject;

namespace Modules.VocabularyModule
{
    // TODO: refactor (split to VocabularyStorageController and VocabularyHolder)
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
            WordDeleteController.OnWordDeleted += SaveVocabularyToStorage;
        }
        
        private void OnDisable()
        {
            WordAddController.OnWordAdded -= SaveVocabularyToStorage;
            WordDeleteController.OnWordDeleted -= SaveVocabularyToStorage;
        }

        private void LoadVocabularyFromStorage()
        {
            Vocabulary = _storageService.Load();
        }
        
        private void SaveVocabularyToStorage()
        {
            _storageService.Save(Vocabulary);
        }
    }
}