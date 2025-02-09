using System.Collections.Generic;

namespace Modules.MiniGamesCore.Abstraction.Models
{
    public class MiniGameQuestionData
    {
        public string Question { get; }
        public List<string> RightAnswers { get; } // list cause of multiple right answers in some games

        public MiniGameQuestionData(string question, List<string> rightAnswers)
        {
            Question = question;
            RightAnswers = rightAnswers;
        }
    }
}