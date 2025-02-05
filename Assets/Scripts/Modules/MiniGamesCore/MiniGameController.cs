using System;
using Modules.ScoreModule;
using Modules.VocabularyModule;
using Modules.VocabularyModule.Data.Models;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Functional.ProgressBar;
using Zenject;

namespace Modules.MiniGamesCore
{
    public abstract class MiniGameController : MonoBehaviour
    {
        [SerializeField] protected int testsPerGame = 10;
        [SerializeField] protected InputField userAnswerField;
        [SerializeField] protected ProgressBar progressBar;
        
        protected Vocabulary Vocabulary;
        protected ScoreController ScoreController;
        protected int CurrentTestIndex;
        
        public event Action OnRightAnswer;
        public event Action<string> OnWrongAnswerWithMessage;
        public static event Action OnWrongAnswer; 
        
        [Inject]
        private void Construct(VocabularyController vocabularyController, ScoreController scoreController)
        {
            Vocabulary = vocabularyController.Vocabulary;
            ScoreController = scoreController;
        }

        private void Start()
        {
            GenerateTests();
            ShowNextTest();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                EvaluateTest();
                CurrentTestIndex++;
                
                userAnswerField.text = string.Empty;
                userAnswerField.ActivateInputField();
                progressBar.SetProgress(CurrentTestIndex, testsPerGame);
                
                if (CurrentTestIndex < testsPerGame)
                {
                    ShowNextTest();
                }
            }
        }
        
        protected abstract void GenerateTests();
        protected abstract void EvaluateTest();
        protected abstract void ShowNextTest();

        protected void InvokeEventsOnRightAnswer()
        {
            OnRightAnswer?.Invoke();
        }

        protected void InvokeEventsOnWrongAnswer(string word)
        {
            OnWrongAnswerWithMessage?.Invoke(word);
            OnWrongAnswer?.Invoke();
        }
    }
}