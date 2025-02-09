using System;
using System.Collections.Generic;
using Constants;
using Modules.MiniGamesCore.TranslationGameModule.Data.Generation;
using Modules.MiniGamesCore.TranslationGameModule.Data.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.MiniGamesCore.TranslationGameModule
{
    public class TranslationGameController : MiniGameController
    {
        [SerializeField] private Text wordToTranslateText;
        [SerializeField] private Text possibleAnswersText;
        
        private List<TranslationGameTestData> _tests;

        private void Start()
        {
            GenerateTests();
        }

        private void GenerateTests()
        {
            var testsGenerator = new TranslationGameTestsGenerator(testsPerGame, Vocabulary);
            _tests = testsGenerator.Generate();
        }

        protected override void ShowNextTest()
        {
            wordToTranslateText.text = _tests[CurrentTestIndex].WordToTranslate;

            // var formattedPossibleAnswers = _tests[CurrentTestIndex].PossibleAnswers
            //     .Select(pa => "[" + pa + "]")
            //     .ToList();
            //
            // possibleAnswersText.text = string.Join(" ", formattedPossibleAnswers);
        }

        protected override void EvaluateTest()
        {
            var input = userAnswerField.text;
            var rightAnswers = _tests[CurrentTestIndex].CorrectAnswers;
            
            if (rightAnswers.Contains(input))
            {
                ScoreController.AddExp(AppConstants.ExpPerTest);
                InvokeEventsOnRightAnswer();
                Vocabulary.ModifyTranslationTestAttemptFor(_tests[CurrentTestIndex].WordToTranslate, true);
            }
            else
            {
                InvokeEventsOnWrongAnswer(string.Join(", ", _tests[CurrentTestIndex].CorrectAnswers));
                Vocabulary.ModifyTranslationTestAttemptFor(_tests[CurrentTestIndex].WordToTranslate, false);
            }
        }
    }
}