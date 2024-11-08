using Constants;
using DG.Tweening;
using Modules.General.Score;
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
            SetInfo();
        }

        private void OnEnable()
        {
            _scoreController.OnExpChanged += SetInfo;
        }
        
        private void OnDisable()
        {
            _scoreController.OnExpChanged -= SetInfo;
        }

        private void SetInfo()
        {
            levelText.text = _scoreController.GetLevel() + " LVL";
            expText.text = _scoreController.GetCurrentLevelExp() + "exp";
            progressBar.SetProgress(_scoreController.GetCurrentLevelExp(), AppConstants.LevelSplitExpCount);
        }
    }
}