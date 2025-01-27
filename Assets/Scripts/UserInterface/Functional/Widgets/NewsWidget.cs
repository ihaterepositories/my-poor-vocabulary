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

        private ScoreController _scoreController;
        
        [Inject]
        private void Construct(ScoreController scoreController)
        {
            _scoreController = scoreController;
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
            news += $"{_scoreController.GetExpCountToReachNewLevel()} exp to reach next level";
            
            newsText.text = news;
        }
    }
}