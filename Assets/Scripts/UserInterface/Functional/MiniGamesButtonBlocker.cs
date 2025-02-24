using Constants;
using DG.Tweening;
using Modules.VocabularyModule;
using Modules.VocabularyModule.Data.Input;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserInterface.Functional
{
    public class MiniGamesButtonBlocker : MonoBehaviour
    {
        [SerializeField] private WordAddController wordAddController;
        [SerializeField] private Image buttonImage;
        
        private VocabularyController _vocabularyController;
        private readonly int _wordsCountToUnlock = AppConstants.WordsCountToUnlockMiniGames;
        
        [Inject]
        private void Construct(VocabularyController vocabularyController)
        {
            _vocabularyController = vocabularyController;
        }

        private void Awake()
        {
            HideButtonIfNeeded();
        }

        private void OnEnable()
        {
            WordAddController.OnWordAdded += CheckWordsCountAndUnlockAnimated;
        }
        
        private void OnDisable()
        {
            WordAddController.OnWordAdded -= CheckWordsCountAndUnlockAnimated;
            buttonImage.DOKill();
        }
        
        private void CheckWordsCountAndUnlockAnimated()
        {
            if (_vocabularyController.Vocabulary.GetCount() == _wordsCountToUnlock)
            {
                buttonImage.DOFade(0f, 1f).OnComplete(() => gameObject.SetActive(false));
            }
        }

        private void HideButtonIfNeeded()
        {
            if (_vocabularyController.Vocabulary.GetCount() >= _wordsCountToUnlock)
            {
                gameObject.SetActive(false);
            }
        }
    }
}