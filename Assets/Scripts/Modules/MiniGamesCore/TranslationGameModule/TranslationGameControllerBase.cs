using System.Collections.Generic;
using System.Linq;
using Constants;
using Modules.MiniGamesCore.TranslationGameModule.Data.Generation;
using Modules.MiniGamesCore.TranslationGameModule.Data.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.MiniGamesCore.TranslationGameModule
{
    public class TranslationGameControllerBase : MiniGameControllerBase
    {
        [SerializeField] private Text wordToTranslateText;
        [SerializeField] private Text possibleAnswersText;
        
        private List<TranslationGameTestData> _tests;
         
        protected override void GenerateTests()
        {
            var testsGenerator = new TranslationGameTestsGenerator(testsPerGame);
            _tests = testsGenerator.Generate();
        }

        protected override void ShowNextTest()
        {
            wordToTranslateText.text = _tests[CurrentTestIndex].WordToTranslate;

            var formattedPossibleAnswers = _tests[CurrentTestIndex].PossibleAnswers
                .Select(pa => "[" + pa + "]")
                .ToList();

            possibleAnswersText.text = string.Join(" ", formattedPossibleAnswers);
        }

        protected override void EvaluateTest()
        {
            var input = userAnswerField.text;
            var rightAnswer = _tests[CurrentTestIndex].CorrectAnswer;
            
            if (input == rightAnswer)
            {
                ScoreController.AddExp(AppConstants.ExpPerTest);
                InvokeEventsOnRightAnswer();
                Vocabulary.ModifyTranslationTestAttemptFor(_tests[CurrentTestIndex].CorrectAnswer, true);
            }
            else
            {
                InvokeEventsOnWrongAnswer(_tests[CurrentTestIndex].CorrectAnswer);
                Vocabulary.ModifyTranslationTestAttemptFor(_tests[CurrentTestIndex].CorrectAnswer, false);
            }
        }
    }
}