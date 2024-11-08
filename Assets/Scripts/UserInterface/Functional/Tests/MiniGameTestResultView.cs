using Constants;
using DG.Tweening;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Functional.Tests
{
    public class MiniGameTestResultView : MonoBehaviour
    {
        [SerializeField] private Text resultText;
        [SerializeField] private MiniGameController miniGameController;

        private void OnEnable()
        {
            miniGameController.OnRightAnswer += ShowRightResult;
            miniGameController.OnWrongAnswer += ShowWrongResult;
        }

        private void OnDisable()
        {
            miniGameController.OnRightAnswer -= ShowRightResult;
            miniGameController.OnWrongAnswer -= ShowWrongResult;
        }

        private void ShowRightResult()
        {
            resultText.DOFlip();
            resultText.text = "+" + AppConstants.ExpPerTest + "exp";
        }
        
        private void ShowWrongResult(string correctWord)
        {
            resultText.DOFlip();
            resultText.text = "Wrong, right answer is " + correctWord + "...";
        }
    }
}