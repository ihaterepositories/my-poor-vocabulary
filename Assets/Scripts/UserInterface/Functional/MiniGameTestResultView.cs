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
        [FormerlySerializedAs("miniGameController")] [SerializeField] private MiniGameControllerBase miniGameControllerBase;

        private void OnEnable()
        {
            miniGameControllerBase.OnRightAnswer += ShowRightResult;
            miniGameControllerBase.OnWrongAnswerWithMessage += ShowOnWrongResult;
        }

        private void OnDisable()
        {
            miniGameControllerBase.OnRightAnswer -= ShowRightResult;
            miniGameControllerBase.OnWrongAnswerWithMessage -= ShowOnWrongResult;
            
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