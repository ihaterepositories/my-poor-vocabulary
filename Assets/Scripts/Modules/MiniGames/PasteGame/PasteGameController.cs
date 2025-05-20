using Constants;
using Modules.MiniGames.Abstraction;
using Modules.MiniGames.PasteGame.Data.Generation;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.MiniGames.PasteGame
{
    public class PasteGameController : MiniGameController
    {
        [SerializeField] private PasteGameQuestionsGenerator questionsGenerator;
        [SerializeField] private Text hintText;
        
        protected override void AssignQuestionsGenerator()
        {
            QuestionsGenerator = questionsGenerator;
        }
        
        protected override void ShowNextTest()
        {
            questionText.text = CurrentQuestion;
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
                ExpController.AddExp(AppConstants.ExpPerTest);
                HandleRightAnswer();
            }
            else
            {
                HandleWrongAnswer(CurrentRightAnswers);
            }
        }
    }
}