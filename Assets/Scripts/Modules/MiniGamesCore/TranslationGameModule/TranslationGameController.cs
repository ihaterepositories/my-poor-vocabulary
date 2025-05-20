using Constants;
using Modules.MiniGamesCore.Abstraction;
using Modules.MiniGamesCore.TranslationGameModule.Data.Generation;
using UnityEngine;

namespace Modules.MiniGamesCore.TranslationGameModule
{
    public class TranslationGameController : MiniGameController
    {
        [SerializeField] private TranslationGameQuestionsGenerator questionsGenerator;

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
                Vocabulary.ModifyTranslationTestAttemptFor(CurrentQuestion, true);
            }
            else
            {
                HandleWrongAnswer(CurrentRightAnswers);
                Vocabulary.ModifyTranslationTestAttemptFor(CurrentQuestion, false);
            }
        }
    }
}