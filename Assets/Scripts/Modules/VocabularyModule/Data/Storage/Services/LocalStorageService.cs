using System.Collections.Generic;
using System.IO;
using Constants;
using Modules.VocabularyModule.Data.Models;
using Modules.VocabularyModule.Data.Storage.Interfaces;
using Newtonsoft.Json;
using UnityEngine;

namespace Modules.VocabularyModule.Data.Storage.Services
{
    public class LocalStorageService : IStorageService
    {
        public Vocabulary Load()
        {
            var json = LoadFileContent();
            var words = new List<Word>();

            if (json != string.Empty)
            {
                // TODO: use auto-mapper
                words = JsonConvert.DeserializeObject<List<Word>>(json);
            }
            
            return new Vocabulary(words);
        }

        public void Save(Vocabulary vocabulary)
        {
            if (vocabulary == null)
            {
                Debug.LogError("Vocabulary is null, can`t save it");
                return;
            }
            
            var words = vocabulary.GetAll();
            
            if (words == null)
            {
                Debug.LogError("Words are null, can`t save vocabulary");
                return;
            }
            
            string json = JsonConvert.SerializeObject(words);
            SaveContentToFile(json);
        }
        
        private void SaveContentToFile(string json)
        {
            CreateStorageIfNotExists();
            File.WriteAllText(AppConstants._localStoragePath, json);
        }
        
        private string LoadFileContent()
        {
            try
            {
                return File.ReadAllText(AppConstants._localStoragePath);
            }
            catch
            {
                CreateStorageIfNotExists();
                return string.Empty;
            }
        }
        
        private void CreateStorageIfNotExists()
        {
            if (!File.Exists(AppConstants._localStoragePath))
            {
                File.Create(AppConstants._localStoragePath).Dispose();
            }
        }
    }
}