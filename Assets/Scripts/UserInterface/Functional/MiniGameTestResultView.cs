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
            resultText.DOFlip();
            resultText.text = "+" + AppConstants.ExpPerTest + "exp";
        }
        
        private void ShowOnWrongResult(List<string> rightAnswers)
        {
            resultText.DOFlip();
            string rightAnswerJoined = rightAnswers.Count > 1 ? string.Join(", ", rightAnswers) : rightAnswers[0];
            resultText.text = "Wrong, right is " + rightAnswerJoined + "...";
        }
    }
}