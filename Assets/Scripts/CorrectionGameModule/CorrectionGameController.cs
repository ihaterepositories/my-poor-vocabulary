using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CorrectionGameModule.Models;
using CorrectionGameModule.TypoGeneration.Interfaces;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using VocabularyModule;
using VocabularyModule.Data.Models;
using Zenject;

namespace CorrectionGameModule
{
    public class CorrectionGameController : MiniGameController
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Text typoWordText;
        [SerializeField] private Text messageText;

        private readonly int _testsPerGame = 10;
        
        private VocabularyController _vocabularyController;
        private Vocabulary Vocabulary => _vocabularyController.Vocabulary;
        private ITypoGenerator _typoGenerator;

        private List<TestData> _tests;
        private string _currentTestWord;
        private string _currentTestTypoWord;
        private int _currentTestIndex;
        
        [Inject]
        private void Construct(VocabularyController vocabularyController, ITypoGenerator typoGenerator)
        {
            _vocabularyController = vocabularyController;
            _typoGenerator = typoGenerator;
        }

        private void Start()
        {
            InitializeTest();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                EvaluateTest();
                _currentTestIndex++;
                
                if (_currentTestIndex < _testsPerGame)
                {
                    ShowNextTest();
                }
                else
                {
                    // TODO: End game
                }
            }
        }

        private async void InitializeTest()
        {
            await GenerateTestData();
            messageText.text = string.Empty;
            ShowNextTest();
        }

        private async Task GenerateTestData()
        {
            _tests = new List<TestData>();
            for (var i = 0; i < _testsPerGame; i++)
            {
                var word = Vocabulary.GetRandomOriginal();
                var typo = await _typoGenerator.GenerateTypo(word);
                _tests.Add(new TestData(word, typo));
            }
        }

        private void ShowNextTest()
        {
            _currentTestWord = _tests[_currentTestIndex].OriginalWord;
            _currentTestTypoWord = _tests[_currentTestIndex].TypoWord;
            typoWordText.text = _currentTestTypoWord;
        }
        
        private void EvaluateTest()
        {
            var input = inputField.text;
            if (input == _currentTestWord)
            {
                // TODO: Add score
                InvokeOnRightAnswer();
            }
            else
            {
                // TODO: message
                InvokeOnWrongAnswer(_currentTestWord);
            }
        }
    }
}