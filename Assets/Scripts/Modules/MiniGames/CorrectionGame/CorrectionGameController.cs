using Constants;
using Modules.MiniGames.Abstraction;
using Modules.MiniGames.CorrectionGame.Data.Generation;
using UnityEngine;

namespace Modules.MiniGames.CorrectionGame
{
    public class CorrectionGameController : MiniGameController
    {
        [SerializeField] private CorrectionGameQuestionsGenerator questionsGenerator;

        protected override void AssignQuestionsGenerator()
        {
            QuestionsGenerator = questionsGenerator;
        }
        
        protected override void ShowNextTest()
        {
            questionText.text = CurrentQuestion;
        }

        protected override void EvaluateTest()
        {
            if (CurrentRightAnswers.Contains(UserAnswer))
            {
                ScoreController.AddExp(AppConstants.ExpPerTest);
                HandleRightAnswer();
            }
            else
            {
                HandleWrongAnswer(CurrentRightAnswers);
            }
        }
    }
}