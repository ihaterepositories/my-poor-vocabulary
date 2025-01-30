using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.VocabularyModule.Data.Input.Validation.UI
{
    public class ValidationMessageView : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color errorColor;

        private void OnDisable()
        {
            text.DOKill();
        }

        public void ShowError(string error)
        {
            text.DOFade(1f, 0.5f);
            text.color = errorColor;
            text.text = error;
        }
        
        public void HideMessage()
        {
            text.DOFade(0f, 0.5f);
        }
    }
}