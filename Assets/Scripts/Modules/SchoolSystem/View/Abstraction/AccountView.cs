using DG.Tweening;
using Modules.SchoolSystem.View.DataModels.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Functional;

namespace Modules.SchoolSystem.View.Abstraction
{
    public abstract class AccountView<T> : MonoBehaviour where T : ISchoolSystemAccount
    {
        [SerializeField] protected GameObject accountUiParent;
        [SerializeField] protected Image transitionImage;

        public void Show(T account)
        {
            ShowUi();
            FillUiFields(account);
            AnimateTransition();
        }

        private void ShowUi()
        {
            accountUiParent.gameObject.SetActive(true);
        }
        
        protected abstract void FillUiFields(T account);

        private void AnimateTransition()
        {
            transitionImage.DOFade(0f, 1f)
                .OnComplete(()=>transitionImage.gameObject.SetActive(false));
        }
    }
}