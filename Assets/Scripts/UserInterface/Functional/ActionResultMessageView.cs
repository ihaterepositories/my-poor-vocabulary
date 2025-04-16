using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Functional
{
    public class ActionResultMessageView : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color errorColor;
        
        private void Awake()
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }

        private void OnDisable()
        {
            text.DOKill();
        }

        public void ShowError(string error)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            text.DOFade(1f, 0.5f).OnComplete(() => text.color = errorColor);
            text.text = error;
        }

        public void ShowSuccess(string success)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            text.DOFade(1f, 0.5f).OnComplete(() => text.color = defaultColor);
            text.text = success;
        }
        
        public void ShowSuccess(string success, float hideDelay)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            text.DOFade(1f, 0.5f).OnComplete(() => text.color = defaultColor);
            text.text = success;
            StartCoroutine(HideWithDelayCoroutine(hideDelay));
        }

        public void ShowProgress(string progress)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            text.DOFade(1f, 0.1f).OnComplete(() => text.color = defaultColor);
            text.text = progress;
        }
        
        private IEnumerator HideWithDelayCoroutine(float hideDelay)
        {
            yield return new WaitForSeconds(hideDelay);
            HideMessage();
        }
        
        public void HideMessage()
        {
            text.DOFade(0f, 0.5f);
        }
    }
}