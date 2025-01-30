using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Functional.ProgressBar
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image progressBar;

        private void OnDisable()
        {
            progressBar.DOKill();
        }

        public void SetProgress(float current, float max)
        {
            progressBar.fillAmount = current / max;
        }
        
        public void SetProgress(float current, float max, float fillDuration)
        {
            progressBar.DOFillAmount(current / max, fillDuration);
        }
    }
}