using Constants;
using Modules.MiniGamesCore.Abstraction;
using Modules.MiniGamesCore.CorrectionGameModule.Data.Generation;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.MiniGamesCore.CorrectionGameModule
{
    public class CorrectionGameController : MiniGameController
    {
        [SerializeField] private CorrectionGameQuestionsGenerator questionsGenerator;
        [SerializeField] private Text typoWordText;

        protected override void AssignQuestionsGenerator()
        {
            QuestionsGenerator = questionsGenerator;
        }
        
        protected override void ShowNextTest()
        {
            typoWordText.text = CurrentQuestion;
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