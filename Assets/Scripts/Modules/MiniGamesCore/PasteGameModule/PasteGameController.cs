using Constants;
using Modules.MiniGamesCore.Abstraction;
using Modules.MiniGamesCore.PasteGameModule.Data.Generation;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.MiniGamesCore.PasteGameModule
{
    public class PasteGameController : MiniGameController
    {
        [SerializeField] private PasteGameQuestionsGenerator questionsGenerator;
        [SerializeField] private Text sentenceText;
        [SerializeField] private Text hintText;
        
        protected override void AssignQuestionsGenerator()
        {
            QuestionsGenerator = questionsGenerator;
        }
        
        protected override void ShowNextTest()
        {
            sentenceText.text = CurrentQuestion;
            hintText.text = GenerateHint();
        }

        private string GenerateHint()
        {
            char[] answerChars = CurrentRightAnswers[0].ToCharArray();
            return answerChars[0] + new string('*', answerChars.Length - 2) + answerChars[^1];
        }

        protected override void EvaluateTest()
        {
            if (CurrentRightAnswers.Contains(UserAnswer))
            {
                ScoreController.AddExp(AppConstants.ExpPerTest);
                InvokeEventsOnRightAnswer();
            }
            else
            {
                InvokeEventsOnWrongAnswer(CurrentRightAnswers);
            }
        }
    }
}