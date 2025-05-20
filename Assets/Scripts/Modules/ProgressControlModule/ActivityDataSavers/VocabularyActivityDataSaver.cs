using System;
using System.Globalization;
using Constants;
using Modules.VocabularyModule;
using Modules.VocabularyModule.Data.Input;
using Modules.VocabularyModule.Data.Models;
using UnityEngine;
using Zenject;

namespace Modules.ProgressControlModule.ActivityDataSavers
{
    public class VocabularyActivityDataSaver : MonoBehaviour
    {
        private Vocabulary _vocabulary;
        
        [Inject]
        private void Construct(VocabularyController vocabularyController)
        {
            _vocabulary = vocabularyController.Vocabulary;
        }

        private void OnEnable()
        {
            WordAddController.OnWordAdded += UpdateVocabularyProgressData;
        }
        
        private void OnDisable()
        {
            WordAddController.OnWordAdded -= UpdateVocabularyProgressData;
        }

        private void UpdateVocabularyProgressData()
        {
            PlayerPrefs.SetInt(AppPlayerPrefsKeys.VocabularyWordsCountKey, _vocabulary.GetWordsCount());
            PlayerPrefs.SetString(AppPlayerPrefsKeys.LastVocabularyUpdateDateKey, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
            PlayerPrefs.Save();
        }
    }
}