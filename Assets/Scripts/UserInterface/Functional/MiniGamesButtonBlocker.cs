using Constants;
using Modules.VocabularyModule;
using Modules.VocabularyModule.Data.Input;
using UnityEngine;
using Zenject;

namespace UserInterface.Functional
{
    public class MiniGamesButtonBlocker : MonoBehaviour
    {
        [SerializeField] private WordAddController wordAddController;
        
        private VocabularyController _vocabularyController;
        private readonly int _wordsCountToUnlock = AppConstants.WordsCountToUnlockMiniGames;
        
        [Inject]
        private void Construct(VocabularyController vocabularyController)
        {
            _vocabularyController = vocabularyController;
        }

        private void Awake()
        {
            CheckWordsCountAndUnlock();
        }

        private void OnEnable()
        {
            WordAddController.OnWordAdded += CheckWordsCountAndUnlock;
        }
        
        private void OnDisable()
        {
            WordAddController.OnWordAdded -= CheckWordsCountAndUnlock;
        }
        
        private void CheckWordsCountAndUnlock()
        {
            if (_vocabularyController.Vocabulary.GetCount() >= _wordsCountToUnlock)
            {
                gameObject.SetActive(false);
            }
        }
    }
}