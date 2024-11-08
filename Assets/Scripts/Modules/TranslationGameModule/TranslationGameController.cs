using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
using Interfaces;
using Modules.General.Navigation;
using Modules.General.Score;
using Modules.TranslationGameModule.Data.Generation;
using Modules.TranslationGameModule.Data.Models;
using Modules.VocabularyModule;
using Modules.VocabularyModule.Data.Models;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Functional.ProgressBar;
using Zenject;

namespace Modules.TranslationGameModule
{
    public class TranslationGameController : MiniGameController
    {
        [SerializeField] private InputField userAnswerField;
        [SerializeField] private Text wordToTranslateText;
        [SerializeField] private Text possibleAnswersText;
        [SerializeField] private ProgressBar progressBar;

        private SceneLoader _sceneLoader;
        private ScoreController _scoreController;
        
        private Vocabulary _vocabulary;
        private TranslationTestsGenerator _dataGenerator;
        
        private readonly int _testsPerGame = 30;
        private List<TranslationTestData> _tests;
        
        private int _currentTestIndex;
        private TranslationTestData _currentTest;

        [Inject]
        private void Construct(
            VocabularyController vocabularyController, 
            SceneLoader sceneLoader,
            ScoreController scoreController)
        {
            _sceneLoader = sceneLoader;
            _vocabulary = vocabularyController.Vocabulary;
            _scoreController = scoreController;
        }
        
        private void Start()
        {
            _dataGenerator = new TranslationTestsGenerator(_vocabulary, _testsPerGame);
            _tests = _dataGenerator.Generate();
            Debug.Log(_tests.Count + " tests generated");
            ShowNextTest();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                EvaluateTest();
                _currentTestIndex++;
                
                userAnswerField.text = string.Empty;
                userAnswerField.ActivateInputField();
                progressBar.SetProgress(_currentTestIndex, _tests.Count);
                
                if (_currentTestIndex < _tests.Count)
                {
                    ShowNextTest();
                }
                else
                {
                    StartCoroutine(_sceneLoader.LoadMenuCoroutine());
                }
            }
        }

        private void ShowNextTest()
        {
            _currentTest = _tests[_currentTestIndex];
            wordToTranslateText.text = _currentTest.WordToTranslate;

            var formattedPossibleAnswers = _currentTest.PossibleAnswers
                .Select(pa => "[" + pa + "]")
                .ToList();

            possibleAnswersText.text = string.Join(" ", formattedPossibleAnswers);
        }

        private void EvaluateTest()
        {
            var input = userAnswerField.text;
            if (input == _currentTest.CorrectAnswer)
            {
                _scoreController.AddExp(AppConstants.ExpPerTest);
                InvokeOnRightAnswer();
                _vocabulary.ModifyTranslationTestAttemptFor(_currentTest.CorrectAnswer, true);
            }
            else
            {
                InvokeOnWrongAnswer(_currentTest.CorrectAnswer);
                _vocabulary.ModifyTranslationTestAttemptFor(_currentTest.CorrectAnswer, false);
            }
        }
    }
}