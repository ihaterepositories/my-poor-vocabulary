using DG.Tweening;
using Modules.PersonalVocabulary;
using Modules.PersonalVocabulary.Data.Input;
using Modules.PersonalVocabulary.Data.Models;
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
            widgetText.DOCounter(0, _vocabulary.GetWordsCount(), 3f);
        }

        private void OnEnable()
        {
            WordAddController.OnWordAdded += SetText;
        }

        private void OnDisable()
        {
            WordAddController.OnWordAdded -= SetText;

            widgetText.DOKill();
        }

        private void SetText()
        {
            widgetText.text = $"{_vocabulary.GetWordsCount()}";
        }
    }
}