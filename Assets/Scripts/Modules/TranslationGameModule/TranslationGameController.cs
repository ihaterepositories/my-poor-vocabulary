using System;
using System.Collections.Generic;
using Interfaces;
using Modules.TranslationGameModule.Data.Generation;
using Modules.TranslationGameModule.Data.Models;
using Modules.VocabularyModule;
using Modules.VocabularyModule.Data.Models;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Functional.ProgressBar;
using UserInterface.Functional.ScenesLoading;
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
        private VocabularyController _vocabularyController;
        
        private Vocabulary Vocabulary => _vocabularyController.Vocabulary;
        private TranslationTestsGenerator _dataGenerator;
        
        private readonly int _testsPerGame = 30;
        private List<TranslationTestData> _tests;
        
        private int _currentTestIndex;
        private TranslationTestData _currentTest;

        [Inject]
        private void Construct(VocabularyController vocabularyController, SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _vocabularyController = vocabularyController;
        }
        
        private void Start()
        {
            _dataGenerator = new TranslationTestsGenerator(Vocabulary, _testsPerGame);
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
                progressBar.SetProgress(_currentTestIndex, _testsPerGame);
                
                if (_currentTestIndex < _testsPerGame)
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
            possibleAnswersText.text = string.Join(" ", _currentTest.PossibleAnswers);
        }

        private void EvaluateTest()
        {
            var input = userAnswerField.text;
            if (input == _currentTest.CorrectAnswer)
            {
                // TODO: Add score
                InvokeOnRightAnswer();
                Vocabulary.ModifyTranslationTestAttemptFor(_currentTest.CorrectAnswer, true);
            }
            else
            {
                InvokeOnWrongAnswer(_currentTest.CorrectAnswer);
                Vocabulary.ModifyTranslationTestAttemptFor(_currentTest.CorrectAnswer, false);
            }
        }
    }
}