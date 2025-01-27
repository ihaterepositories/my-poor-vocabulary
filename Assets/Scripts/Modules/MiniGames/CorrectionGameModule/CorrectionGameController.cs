using System.Collections.Generic;
using Constants;
using Modules.MiniGames.CorrectionGameModule.Data.Generation.Generation;
using Modules.MiniGames.CorrectionGameModule.Data.Models;
using Modules.MiniGames.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.MiniGames.CorrectionGameModule
{
    public class CorrectionGameController : MiniGameController
    {
        [SerializeField] private Text typoWordText;
        [SerializeField] private Text messageText;

        private List<CorrectionGameTestData> _tests;
        
        protected override void GenerateTests()
        {
            var testsGenerator = new CorrectionTestsGenerator(Vocabulary, testsPerGame);
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