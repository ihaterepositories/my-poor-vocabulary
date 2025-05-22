using System;
using Modules.PersonalVocabulary.Data.Delete;
using Modules.PersonalVocabulary.Data.Input;
using Modules.PersonalVocabulary.Data.Models;
using Modules.PersonalVocabulary.Data.Storage.Interfaces;
using UnityEngine;
using Zenject;

namespace Modules.PersonalVocabulary
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

        private void OnApplicationQuit()
        {
            SaveVocabularyToStorage();
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