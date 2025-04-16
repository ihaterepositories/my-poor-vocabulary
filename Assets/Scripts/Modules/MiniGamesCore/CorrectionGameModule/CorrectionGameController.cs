using Constants;
using Modules.MiniGamesCore.Abstraction;
using Modules.MiniGamesCore.CorrectionGameModule.Data.Generation;
using UnityEngine;

namespace Modules.MiniGamesCore.CorrectionGameModule
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
                InvokeEventsOnRightAnswer();
            }
            else
            {
                InvokeEventsOnWrongAnswer(CurrentRightAnswers);
            }
        }
    }
}