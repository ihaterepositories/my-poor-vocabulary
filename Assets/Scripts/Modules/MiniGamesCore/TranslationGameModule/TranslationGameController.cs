using Constants;
using Modules.MiniGamesCore.Abstraction;
using Modules.MiniGamesCore.TranslationGameModule.Data.Generation;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.MiniGamesCore.TranslationGameModule
{
    public class TranslationGameController : MiniGameController
    {
        [SerializeField] private TranslationGameQuestionsGenerator questionsGenerator;
        [SerializeField] private Text wordToTranslateText;

        protected override void AssignQuestionsGenerator()
        {
            QuestionsGenerator = questionsGenerator;
        }
        
        protected override void ShowNextTest()
        {
            wordToTranslateText.text = CurrentQuestion;
        }

        protected override void EvaluateTest()
        {
            if (CurrentRightAnswers.Contains(UserAnswer))
            {
                ScoreController.AddExp(AppConstants.ExpPerTest);
                InvokeEventsOnRightAnswer();
                Vocabulary.ModifyTranslationTestAttemptFor(CurrentQuestion, true);
            }
            else
            {
                InvokeEventsOnWrongAnswer(CurrentRightAnswers);
                Vocabulary.ModifyTranslationTestAttemptFor(CurrentQuestion, false);
            }
        }
    }
}