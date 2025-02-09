using System;
using System.Collections.Generic;
using Modules.MiniGamesCore.Abstraction.Models;

namespace Modules.MiniGamesCore.Abstraction.Interfaces
{
    public interface IMiniGameQuestionsGenerator
    {
        public void Generate(Action<List<MiniGameQuestionData>> onCompleteCallback, int testsCount);
    }
}