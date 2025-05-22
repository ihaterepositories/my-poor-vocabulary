using System;
using Modules.PersonalVocabulary;
using Modules.PersonalVocabulary.Data.Models;
using TMPro;
using UnityEngine;
using Zenject;

namespace Modules.ActivityAnalytic.View
{
    public class ProblematicWordsListView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI problematicWordsText;
        private Vocabulary _vocabulary;
        
        [Inject]
        private void Construct(VocabularyController vocabularyController)
        {
            _vocabulary = vocabularyController.Vocabulary;
        }

        private void Start()
        {
            ShowProblematicWords();
        }

        private void ShowProblematicWords()
        {
            var problematicWords = _vocabulary.GetProblematicWords();
            if (problematicWords.Count == 0)
            {
                problematicWordsText.text = "No problematic words found.";
                return;
            }

            foreach (var pw in problematicWords)
            {
                problematicWordsText.text += $"{pw.Original} - ";
                foreach (var translation in pw.Translations)
                {
                    problematicWordsText.text += $"{translation}, ";
                }
                problematicWordsText.text = problematicWordsText.text.TrimEnd(',', ' ') + "\n";
            }
        }
    }
}