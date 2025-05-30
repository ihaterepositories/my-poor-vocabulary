using System;
using System.Globalization;
using Constants;
using Modules.PersonalVocabulary;
using Modules.PersonalVocabulary.Data.Input;
using Modules.PersonalVocabulary.Data.Models;
using UnityEngine;
using Zenject;

namespace Modules.ActivityAnalytic.ActivityDataSavers
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
            WordAddController.OnWordAdded += UpdateVocabularyActivityData;
        }
        
        private void OnDisable()
        {
            WordAddController.OnWordAdded -= UpdateVocabularyActivityData;
        }

        private void UpdateVocabularyActivityData()
        {
            PlayerPrefs.SetInt(AppPlayerPrefsKeys.VocabularyWordsCountKey, _vocabulary.GetWordsCount());
            PlayerPrefs.SetString(AppPlayerPrefsKeys.LastVocabularyUpdateDateKey, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
            PlayerPrefs.Save();
        }
    }
}