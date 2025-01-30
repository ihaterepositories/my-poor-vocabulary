using System.Collections;
using Constants;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Animators.Custom;

namespace UserInterface.Functional.TipsCore
{
    public class TipView : MonoBehaviour
    {
        [SerializeField] private Text viewText;
        [SerializeField] private TipViewAnimator tipViewAnimator;

        private readonly float _showDelay = 1f;
        
        public void Show(string tip)
        {
            if (!CheckTipText(tip)) return;
            StartCoroutine(ShowCoroutine(tip));
        }
        
        private IEnumerator ShowCoroutine(string tip)
        {
            yield return new WaitForSeconds(_showDelay);
            viewText.text = tip;
            tipViewAnimator.Show();
        }
        
        public void Hide()
        {
            StopAllCoroutines();
            viewText.text = string.Empty;
            tipViewAnimator.Hide();
        }
        
        private bool CheckTipText(string tip)
        {
            if (tip.Length is not (> AppConstants.MaxTipLength or 0)) return true;
            Debug.LogError("Tip can`t be showed, text is too long or empty");
            return false;
        }
    }
}