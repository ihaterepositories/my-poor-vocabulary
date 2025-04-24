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

        private Color _defaultTransparentColor;
        private Color _errorTransparentColor;
        
        private void Awake()
        {
            _defaultTransparentColor = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0);
            _errorTransparentColor = new Color(errorColor.r, errorColor.g, errorColor.b, 0);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }

        private void OnDisable()
        {
            text.DOKill();
        }

        public void ShowError(string error)
        {
            text.color = _errorTransparentColor;
            text.DOFade(1f, 0.5f).OnComplete(() => text.color = errorColor);
            text.text = error;
        }

        public void ShowSuccess(string success)
        {
            text.color = _defaultTransparentColor;
            text.DOFade(1f, 0.5f).OnComplete(() => text.color = defaultColor);
            text.text = success;
        }
        
        public void ShowSuccess(string success, float hideDelay)
        {
            text.color = _defaultTransparentColor;
            text.DOFade(1f, 0.5f).OnComplete(() => text.color = defaultColor);
            text.text = success;
            StartCoroutine(HideWithDelayCoroutine(hideDelay));
        }

        public void ShowProgress(string progress)
        {
            text.color = _defaultTransparentColor;
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