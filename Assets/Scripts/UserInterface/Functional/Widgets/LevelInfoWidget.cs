using Constants;
using DG.Tweening;
using Modules.ScoreModule;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserInterface.Functional.Widgets
{
    public class LevelInfoWidget : MonoBehaviour
    {
        [SerializeField] private Text levelText;
        [SerializeField] private Text expText;
        [SerializeField] private ProgressBar.ProgressBar progressBar;
        
        private ExpController _expController;
        
        [Inject]
        private void Construct(ExpController expController)
        {
            _expController = expController;
        }

        private void Start()
        {
            levelText.DOCounter(0,_expController.GetLevel(), 1f);
            progressBar.SetProgress(_expController.GetCurrentLevelExp(), AppConstants.LevelSplitExpCount, 1f);
            expText.DOCounter(0, _expController.GetCurrentLevelExp(), 1f)
                .OnComplete(SetInfo);
        }

        private void OnEnable()
        {
            ExpController.OnExpChanged += SetInfo;
        }
        
        private void OnDisable()
        {
            ExpController.OnExpChanged -= SetInfo;

            levelText.DOKill();
            expText.DOKill();
            progressBar.DOKill();
        }

        private void SetInfo()
        {
            levelText.text = _expController.GetLevel() + " LVL";
            expText.text = _expController.GetCurrentLevelExp() + "exp";
            progressBar.SetProgress(_expController.GetCurrentLevelExp(), AppConstants.LevelSplitExpCount);
        }
    }
}