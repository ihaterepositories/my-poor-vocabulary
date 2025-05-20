using System;
using Modules.ScoreModule;
using Modules.VocabularyModule.Data.Input;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserInterface.Functional.Widgets
{
    // TODO: create constants for news strings
    public class NewsWidget : MonoBehaviour
    {
        [SerializeField] private Text newsText;

        private ExpController _expController;
        
        [Inject]
        private void Construct(ExpController expController)
        {
            _expController = expController;
        }
        
        private void Start()
        {
            SetNews();
        }

        private void OnEnable()
        {
            WordAddController.OnWordAdded += SetNews;
        }
        
        private void OnDisable()
        {
            WordAddController.OnWordAdded -= SetNews;
        }

        private void SetNews()
        {
            string news = String.Empty;
            
            news += "New mini games is coming soon! • ";
            news += $"{_expController.GetExpCountToReachNewLevel()} exp to reach next level";
            
            newsText.text = news;
        }
    }
}