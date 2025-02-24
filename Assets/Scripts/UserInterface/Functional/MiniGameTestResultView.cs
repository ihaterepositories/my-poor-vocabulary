using System;
using System.Collections.Generic;
using Constants;
using DG.Tweening;
using Modules.MiniGamesCore.Abstraction;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Functional
{
    public class MiniGameTestResultView : MonoBehaviour
    {
        [SerializeField] private Color rightAnswerColor;
        [SerializeField] private Color wrongAnswerColor;
        [SerializeField] private Text resultText;
        [SerializeField] private MiniGameController miniGameController;

        private void OnEnable()
        {
            miniGameController.OnRightAnswer += ShowRightResult;
            miniGameController.OnWrongAnswerWithMessage += ShowOnWrongResult;
        }

        private void OnDisable()
        {
            miniGameController.OnRightAnswer -= ShowRightResult;
            miniGameController.OnWrongAnswerWithMessage -= ShowOnWrongResult;
            
            resultText.DOKill();
        }

        private void ShowRightResult()
        {
            resultText.DOText("Last answer: ", 0.2f).OnComplete(()=>
                resultText.DOText($"Last answer: right, + {AppConstants.ExpPerTest} exp.", 0.75f));
        }
        
        private void ShowOnWrongResult(List<string> rightAnswers)
        {
            string rightAnswerJoined = rightAnswers.Count > 1 ? string.Join(", ", rightAnswers) : rightAnswers[0];
            resultText.DOText("Last answer: ", 0.2f).OnComplete(()=>
                resultText.DOText($"Last answer: wrong, right answer is {rightAnswerJoined}.", 0.75f));
        }
    }
}