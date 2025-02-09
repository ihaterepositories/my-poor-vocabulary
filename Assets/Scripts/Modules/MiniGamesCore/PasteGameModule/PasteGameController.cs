using System.Collections.Generic;
using Constants;
using Modules.MiniGamesCore.PasteGameModule.Data.Generation;
using Modules.MiniGamesCore.PasteGameModule.Data.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.MiniGamesCore.PasteGameModule
{
    public class PasteGameController : MiniGameController
    {
        [SerializeField] private PasteGameTestsGenerator pasteGameTestsGenerator;
        [SerializeField] private Text sentenceText;
        [SerializeField] private Text hintText;
        [SerializeField] private Text messageText;
        
        private List<PasteGameTestData> _tests;

        private void Start()
        {
            userAnswerField.gameObject.SetActive(false);
            messageText.text = PlayerPrefs.GetString("PickedPasteGameCharacter", "Someone") + " is writing questions, please wait...";
            GenerateTests();
        }
        
        private void GenerateTests()
        {
            pasteGameTestsGenerator.Generate(StartGame, testsPerGame);
        }

        private void StartGame(List<PasteGameTestData> tests)
        {
            messageText.text = string.Empty;
            _tests = tests;
            ShowNextTest();
            userAnswerField.gameObject.SetActive(true);
        }
        
        protected override void ShowNextTest()
        {
            sentenceText.text = _tests[CurrentTestIndex].Sentence;
            hintText.text = GenerateHint();
        }

        private string GenerateHint()
        {
            char[] answerChars = _tests[CurrentTestIndex].WordToPaste.ToCharArray();
            return answerChars[0] + new string('*', answerChars.Length - 2) + answerChars[^1];
        }

        protected override void EvaluateTest()
        {
            var userAnswer = userAnswerField.text;
            var rightAnswer = _tests[CurrentTestIndex].WordToPaste;
            
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