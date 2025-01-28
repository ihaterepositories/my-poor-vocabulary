using System.Collections.Generic;
using Constants;
using Modules.MiniGames.CorrectionGameModule.Data.Generation;
using Modules.MiniGames.CorrectionGameModule.Data.Generation.TypoGenerators.Interfaces;
using Modules.MiniGames.CorrectionGameModule.Data.Models;
using Modules.MiniGames.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.MiniGames.CorrectionGameModule
{
    public class CorrectionGameController : MiniGameController
    {
        [SerializeField] private Text typoWordText;

        private IAsyncTypoGenerator _asyncTypoGenerator;
        private List<CorrectionGameTestData> _tests;
        
        [Inject]
        private void Construct(IAsyncTypoGenerator asyncTypoGenerator)
        {
            _asyncTypoGenerator = asyncTypoGenerator;
        }
        
        protected override void GenerateTests()
        {
            var testsGenerator = new CorrectionGameTestsGenerator(_asyncTypoGenerator, Vocabulary, testsPerGame);
            _tests = testsGenerator.Generate();
        }

        protected override void ShowNextTest()
        {
            typoWordText.text = _tests[CurrentTestIndex].TypoWord;
        }
        
        protected override void EvaluateTest()
        {
            var input = userAnswerField.text;
            if (input == _tests[CurrentTestIndex].OriginalWord)
            {
                ScoreController.AddExp(AppConstants.ExpPerTest);
                InvokeEventsOnRightAnswer();
            }
            else
            {
                InvokeEventsOnWrongAnswer(_tests[CurrentTestIndex].OriginalWord);
            }
        }
    }
}