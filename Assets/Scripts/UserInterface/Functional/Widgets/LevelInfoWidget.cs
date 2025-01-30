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
        
        private ScoreController _scoreController;
        
        [Inject]
        private void Construct(ScoreController scoreController)
        {
            _scoreController = scoreController;
        }

        private void Start()
        {
            levelText.DOCounter(0,_scoreController.GetLevel(), 3f);
            progressBar.SetProgress(_scoreController.GetCurrentLevelExp(), AppConstants.LevelSplitExpCount, 3f);
            expText.DOCounter(0, _scoreController.GetCurrentLevelExp(), 3f)
                .OnComplete(SetInfo);
        }

        private void OnEnable()
        {
            ScoreController.OnExpChanged += SetInfo;
        }
        
        private void OnDisable()
        {
            ScoreController.OnExpChanged -= SetInfo;

            levelText.DOKill();
            expText.DOKill();
            progressBar.DOKill();
        }

        private void SetInfo()
        {
            levelText.text = _scoreController.GetLevel() + " LVL";
            expText.text = _scoreController.GetCurrentLevelExp() + "exp";
            progressBar.SetProgress(_scoreController.GetCurrentLevelExp(), AppConstants.LevelSplitExpCount);
        }
    }
}