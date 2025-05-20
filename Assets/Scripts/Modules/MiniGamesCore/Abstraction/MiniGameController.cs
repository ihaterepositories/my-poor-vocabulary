using System;
using System.Collections.Generic;
using DG.Tweening;
using Modules.MiniGamesCore.Abstraction.Interfaces;
using Modules.MiniGamesCore.Abstraction.Models;
using Modules.ScoreModule;
using Modules.VocabularyModule;
using Modules.VocabularyModule.Data.Models;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Functional.ProgressBar;
using Zenject;

namespace Modules.MiniGamesCore.Abstraction
{
    public abstract class MiniGameController : MonoBehaviour
    {
        [SerializeField] protected int testsPerGame = 10;
        [SerializeField] protected Text questionText;
        [SerializeField] protected InputField userAnswerField;
        [SerializeField] protected ProgressBar progressBar;
        [SerializeField] protected Text messageText;
        
        protected IMiniGameQuestionsGenerator QuestionsGenerator;
        protected string UserAnswer => userAnswerField.text;
        protected string CurrentQuestion => _questions[_currentQuestionIndex].Question;
        protected List<string> CurrentRightAnswers => _questions[_currentQuestionIndex].RightAnswers;
        protected Vocabulary Vocabulary;
        protected ScoreController ScoreController;

        private List<MiniGameQuestionData> _questions;
        private int _currentQuestionIndex;
        private int _rightAnsweredQuestionsCount;
        private bool _isGameEnd;
        
        public event Action OnRightAnswer;
        public event Action<List<string>> OnWrongAnswerWithMessage;
        public static event Action OnWrongAnswer; 
        public static event Action<int> OnGameEndWithScore;
        
        [Inject]
        private void Construct(VocabularyController vocabularyController, ScoreController scoreController)
        {
            Vocabulary = vocabularyController.Vocabulary;
            ScoreController = scoreController;
        }

        private void Awake()
        {
            AssignQuestionsGenerator();
        }

        private void Start()
        {
            userAnswerField.gameObject.SetActive(false);
            GenerateQuestions();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) && !_isGameEnd)
            {
                EvaluateTest();
                _currentQuestionIndex++;
                
                RefreshInputField();
                UpdateProgressBar();
                
                if (_currentQuestionIndex < testsPerGame)
                {
                    ShowNextTestAnimated();
                }
                else
                {
                    _isGameEnd = true;
                    OnGameEndWithScore?.Invoke((_rightAnsweredQuestionsCount*100) / testsPerGame);
                    // userAnswerField.gameObject.SetActive(false);
                }
            }
        }

        private void OnDisable()
        {
            questionText.DOKill();
        }

        protected abstract void AssignQuestionsGenerator();
        
        private void GenerateQuestions()
        {
            if (QuestionsGenerator == null)
            {
                Debug.LogError("Questions generator is not assigned!");
                return;
            }
            
            QuestionsGenerator.Generate(StartGame, testsPerGame);
        }
        
        protected abstract void EvaluateTest();
        
        private void RefreshInputField()
        {
            userAnswerField.text = string.Empty;
            userAnswerField.ActivateInputField();
        }

        private void UpdateProgressBar()
        {
            if (_currentQuestionIndex <= testsPerGame)
                progressBar.SetProgress(_currentQuestionIndex, testsPerGame);
        }

        private void ShowNextTestAnimated()
        {
            questionText
                .DOFade(0f, 0.5f)
                .OnComplete(() => {
                    ShowNextTest();
                    questionText.DOFade(1f, 0.5f);
                });
        }
        
        protected abstract void ShowNextTest();

        private void StartGame(List<MiniGameQuestionData> questions)
        {
            messageText.text = string.Empty;
            _questions = questions;
            ShowNextTest();
            userAnswerField.gameObject.SetActive(true);
        }

        protected void HandleRightAnswer()
        {
            _rightAnsweredQuestionsCount++;
            OnRightAnswer?.Invoke();
        }

        protected void HandleWrongAnswer(List<string> word)
        {
            OnWrongAnswerWithMessage?.Invoke(word);
            OnWrongAnswer?.Invoke();
        }
    }
}