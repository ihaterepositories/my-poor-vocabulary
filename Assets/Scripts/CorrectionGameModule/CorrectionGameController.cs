using System.Collections.Generic;
using System.Threading.Tasks;
using CorrectionGameModule.Models;
using CorrectionGameModule.TypoGeneration.Interfaces;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Functional.ProgressBar;
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
        [SerializeField] private ProgressBar progressBar;

        private readonly int _testsPerGame = 10;
        
        private VocabularyController _vocabularyController;
        private Vocabulary Vocabulary => _vocabularyController.Vocabulary;
        private IAsyncTypoGenerator _asyncTypoGenerator;

        private List<TestData> _tests;
        private string _currentTestWord;
        private string _currentTestTypoWord;
        private int _currentTestIndex;
        
        [Inject]
        private void Construct(VocabularyController vocabularyController, IAsyncTypoGenerator asyncTypoGenerator)
        {
            _vocabularyController = vocabularyController;
            _asyncTypoGenerator = asyncTypoGenerator;
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
                
                inputField.text = string.Empty;
                inputField.ActivateInputField();
                progressBar.SetProgress(_currentTestIndex, _testsPerGame);
                
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
                var typo = await _asyncTypoGenerator.GenerateTypo(word);
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
                InvokeOnWrongAnswer(_currentTestWord);
            }
        }
    }
}