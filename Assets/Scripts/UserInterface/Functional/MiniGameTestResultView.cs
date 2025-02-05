using Constants;
using DG.Tweening;
using Modules.MiniGamesCore;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UserInterface.Functional
{
    public class MiniGameTestResultView : MonoBehaviour
    {
        [SerializeField] private Text resultText;
        [FormerlySerializedAs("miniGameControllerBase")] [SerializeField] private MiniGameController miniGameController;

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
        
        private void ShowOnWrongResult(string correctWord)
        {
            resultText.DOFlip();
            resultText.text = "Wrong, right answer is " + correctWord + "...";
        }
    }
}