using System.IO;
using Constants;
using Newtonsoft.Json;
using UnityEngine;
using VocabularyModule.Data.Models;
using VocabularyModule.Data.Storage.Interfaces;

namespace VocabularyModule.Data.Storage.Services
{
    public class LocalStorageService : IStorageService
    {
        public Vocabulary Load()
        {
            var vocabulary = new Vocabulary();
            var json = LoadFileContent() ?? string.Empty;

            if (json != string.Empty)
            {
                vocabulary = JsonConvert.DeserializeObject<Vocabulary>(json);
            }
            
            return vocabulary;
        }

        public void Save(Vocabulary vocabulary)
        {
            if (vocabulary == null)
            {
                Debug.LogError("Vocabulary is null, can`t save it");
                return;
            }
            
            string json = JsonConvert.SerializeObject(vocabulary);
            SaveContentToFile(json);
        }
        
        private void SaveContentToFile(string json)
        {
            CreateStorageIfNotExists();
            File.WriteAllText(AppConstants.LocalStorageFolder+"/vocabulary.json", json);
        }
        
        private string LoadFileContent()
        {
            CreateStorageIfNotExists();
            return File.ReadAllText(AppConstants.LocalStorageFolder+"/vocabulary.json");
        }
        
        private void CreateStorageIfNotExists()
        {
            if (Directory.Exists(AppConstants.LocalStorageFolder)) return;
            Directory.CreateDirectory(AppConstants.LocalStorageFolder);
            File.WriteAllText(AppConstants.LocalStorageFolder+"/vocabulary.json", string.Empty);
        }
    }
}