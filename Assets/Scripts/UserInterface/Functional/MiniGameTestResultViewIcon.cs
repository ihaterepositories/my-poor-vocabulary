using System;
using DG.Tweening;
using Modules.MiniGamesCore.Abstraction;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Functional
{
    public class MiniGameTestResultViewIcon : MonoBehaviour
    {
        [SerializeField] private Image answerImageInstance;
        [SerializeField] private RectTransform answerImageInstanceRect;
        [SerializeField] private Sprite rightAnswerImage;
        [SerializeField] private Sprite wrongAnswerImage;
        [SerializeField] private MiniGameController miniGameController;
        
        private void OnEnable()
        {
            miniGameController.OnRightAnswer += ShowRightResult;
            MiniGameController.OnWrongAnswer += ShowOnWrongResult;
        }

        private void OnDisable()
        {
            miniGameController.OnRightAnswer -= ShowRightResult;
            MiniGameController.OnWrongAnswer -= ShowOnWrongResult;

            answerImageInstanceRect.DOKill();
        }

        private void ShowRightResult()
        {
            answerImageInstanceRect.DOScale(0f, 0.25f).OnComplete(() =>
            {
                answerImageInstance.sprite = rightAnswerImage;
                answerImageInstanceRect.DOScale(1f, 0.25f);
            });
        }
        
        private void ShowOnWrongResult()
        {
            answerImageInstanceRect.DOScale(0f, 0.25f).OnComplete(() =>
            {
                answerImageInstance.sprite = wrongAnswerImage;
                answerImageInstanceRect.DOScale(1f, 0.25f);
            });
        }
    }
}