using System;
using UnityEngine;

namespace Modules.MiniGames.Interfaces
{
    // TODO: generalize more things from child controllers
    public abstract class MiniGameController : MonoBehaviour
    {
        public event Action OnRightAnswer;
        public event Action<string> OnPassRightAnswer;
        public static event Action OnWrongAnswer; 
        
        protected void InvokeOnRightAnswer()
        {
            OnRightAnswer?.Invoke();
        }

        protected void InvokeOnWrongAnswer(string word)
        {
            OnPassRightAnswer?.Invoke(word);
            OnWrongAnswer?.Invoke();
        }
    }
}