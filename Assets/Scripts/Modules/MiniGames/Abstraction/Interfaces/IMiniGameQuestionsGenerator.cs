using System;
using System.Collections.Generic;
using Modules.MiniGames.Abstraction.Models;

namespace Modules.MiniGames.Abstraction.Interfaces
{
    public interface IMiniGameQuestionsGenerator
    {
        public void Generate(Action<List<MiniGameQuestionData>> onCompleteCallback, int testsCount);
    }
}