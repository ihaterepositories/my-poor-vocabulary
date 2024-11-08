using System;
using Modules.VocabularyModule;
using Modules.VocabularyModule.Data.Input;
using Modules.VocabularyModule.Data.Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserInterface.Functional.Widgets
{
    public class VocabularyCapacityWidget : MonoBehaviour
    {
        [SerializeField] private Text widgetText;
        
        private Vocabulary _vocabulary;
        
        [Inject]
        private void Construct(VocabularyController vocabularyController)
        {
            _vocabulary = vocabularyController.Vocabulary;
        }

        private void Start()
        {
            SetText();
        }

        private void OnEnable()
        {
            WordAddController.OnWordAdded += SetText;
        }

        private void OnDisable()
        {
            WordAddController.OnWordAdded -= SetText;
        }

        private void SetText()
        {
            widgetText.text = $"{_vocabulary.GetCount()} words inside";
        }
    }
}