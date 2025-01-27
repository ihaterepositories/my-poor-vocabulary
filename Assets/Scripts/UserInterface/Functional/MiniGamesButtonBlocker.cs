using Modules.VocabularyModule;
using Modules.VocabularyModule.Data.Input;
using UnityEngine;
using Zenject;

namespace UserInterface.Functional
{
    public class MiniGamesButtonBlocker : MonoBehaviour
    {
        [SerializeField] private int wordsCountToUnlock = 49;
        [SerializeField] private WordAddController wordAddController;
        private VocabularyController _vocabularyController;
        
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
            if (_vocabularyController.Vocabulary.GetCount() > wordsCountToUnlock)
            {
                gameObject.SetActive(false);
            }
        }
    }
}