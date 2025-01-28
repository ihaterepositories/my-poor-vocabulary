using System.Collections.Generic;
using Constants;
using Modules.MiniGames.Interfaces;
using Modules.MiniGames.PasteGameModule.Data.Generation;
using Modules.MiniGames.PasteGameModule.Data.Generation.SentenceGenerators.Interfaces;
using Modules.MiniGames.PasteGameModule.Data.Models;
using Modules.VocabularyModule;
using Modules.VocabularyModule.Data.Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.MiniGames.PasteGameModule
{
    public class PasteGameController : MiniGameController
    {
        [SerializeField] private Text sentenceText;
        private List<PasteGameTestData> _tests;
        private IAsyncSentenceGenerator _sentenceGenerator;

        [Inject]
        private void Construct(IAsyncSentenceGenerator sentenceGenerator)
        {
            _sentenceGenerator = sentenceGenerator;
        }
        
        protected override void GenerateTests()
        {
            var testsGenerator = new PasteGameTestsGenerator(_sentenceGenerator, Vocabulary, testsPerGame);
            _tests = testsGenerator.Generate();
        }
        
        protected override void ShowNextTest()
        {
            sentenceText.text = _tests[CurrentTestIndex].Sentence;
        }

        protected override void EvaluateTest()
        {
            var userAnswer = userAnswerField.text;
            var rightAnswer = _tests[CurrentTestIndex].Sentence;
            
            if (userAnswer == rightAnswer)
            {
                ScoreController.AddExp(AppConstants.ExpPerTest);
                InvokeEventsOnRightAnswer();
            }
            else
            {
                InvokeEventsOnWrongAnswer(_tests[CurrentTestIndex].WordToPaste);
            }
        }
    }
}