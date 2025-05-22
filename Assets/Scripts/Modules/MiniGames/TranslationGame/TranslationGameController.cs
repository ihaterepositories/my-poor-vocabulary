using Constants;
using Modules.MiniGames.Abstraction;
using Modules.MiniGames.TranslationGame.Data.Generation;
using UnityEngine;

namespace Modules.MiniGames.TranslationGame
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
                ExpController.AddExp(AppConstants.ExpPerTest);
                HandleRightAnswer();
                Vocabulary.ModifyProblematicStatusFor(CurrentQuestion, true);
            }
            else
            {
                HandleWrongAnswer(CurrentRightAnswers);
                Vocabulary.ModifyProblematicStatusFor(CurrentQuestion, false);
            }
        }
    }
}