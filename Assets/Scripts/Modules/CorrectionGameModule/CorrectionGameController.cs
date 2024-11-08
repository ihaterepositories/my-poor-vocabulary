using System.Collections.Generic;
using System.Threading.Tasks;
using Constants;
using Interfaces;
using Modules.CorrectionGameModule.Models;
using Modules.CorrectionGameModule.TypoGeneration.Interfaces;
using Modules.General.Navigation;
using Modules.General.Score;
using Modules.VocabularyModule;
using Modules.VocabularyModule.Data.Models;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Functional.ProgressBar;
using Zenject;

namespace Modules.CorrectionGameModule
{
    // TODO: refactor -> make CorrectionTestsGenerator
    public class CorrectionGameController : MiniGameController
    {
        [SerializeField] private InputField userAnswerField;
        [SerializeField] private Text typoWordText;
        [SerializeField] private Text messageText;
        [SerializeField] private ProgressBar progressBar;

        private readonly int _testsPerGame = 10;
        
        private SceneLoader _sceneLoader;
        private ScoreController _scoreController;
        
        private Vocabulary _vocabulary;
        private IAsyncTypoGenerator _asyncTypoGenerator;

        private List<TestData> _tests;
        private string _currentTestWord;
        private string _currentTestTypoWord;
        private int _currentTestIndex;
        
        [Inject]
        private void Construct(
            VocabularyController vocabularyController, 
            IAsyncTypoGenerator asyncTypoGenerator, 
            SceneLoader sceneLoader,
            ScoreController scoreController)
        {
            _vocabulary = vocabularyController.Vocabulary;
            _asyncTypoGenerator = asyncTypoGenerator;
            _sceneLoader = sceneLoader;
            _scoreController = scoreController;
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

        private async void InitializeTest()
        {
            await GenerateTestsData();
            messageText.text = string.Empty;
            ShowNextTest();
        }

        private async Task GenerateTestsData()
        {
            _tests = new List<TestData>();
            for (var i = 0; i < _testsPerGame; i++)
            {
                var word = _vocabulary.GetRandom().Original;
                
                if (word.Contains(' '))
                {
                    i--;
                    continue;
                }
                
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
            var input = userAnswerField.text;
            if (input == _currentTestWord)
            {
                _scoreController.AddExp(AppConstants.ExpPerTest);
                InvokeOnRightAnswer();
            }
            else
            {
                InvokeOnWrongAnswer(_currentTestWord);
            }
        }
    }
}