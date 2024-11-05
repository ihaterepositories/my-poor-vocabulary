using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Functional.ProgressBar
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image progressBar;
        
        public void SetProgress(float current, float max)
        {
            progressBar.fillAmount = current / max;
        }
    }
}