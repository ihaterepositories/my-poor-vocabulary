using System;
using UnityEngine;

namespace Interfaces
{
    public abstract class MiniGameController : MonoBehaviour
    {
        public event Action OnRightAnswer;
        public event Action<string> OnWrongAnswer;
        
        protected void InvokeOnRightAnswer()
        {
            OnRightAnswer?.Invoke();
        }

        protected void InvokeOnWrongAnswer(string word)
        {
            OnWrongAnswer?.Invoke(word);
        }
    }
}