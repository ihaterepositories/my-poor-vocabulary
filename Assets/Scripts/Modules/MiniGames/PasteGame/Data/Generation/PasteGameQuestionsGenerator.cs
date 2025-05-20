using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Modules.MiniGames.Abstraction.Interfaces;
using Modules.MiniGames.Abstraction.Models;
using Modules.MiniGames.PasteGame.Data.Factories;
using Modules.MiniGames.PasteGame.Data.Generation.SentenceGenerators.Interfaces;
using Modules.VocabularyModule;
using Modules.VocabularyModule.Data.Models;
using UnityEngine;
using Zenject;

namespace Modules.MiniGames.PasteGame.Data.Generation
{
    public class PasteGameQuestionsGenerator : MonoBehaviour, IMiniGameQuestionsGenerator
    {
        private IAsyncSentenceGenerator _sentenceGenerator;
        private Vocabulary _vocabulary;
        private int _testsCount;
        private string _characterDescription;
        
        [Inject]
        private void Construct(IAsyncSentenceGenerator asyncSentenceGenerator, VocabularyController vocabularyController)
        {
            _vocabulary = vocabularyController.Vocabulary;
            _sentenceGenerator = asyncSentenceGenerator;
        }
        
        public void Generate(Action<List<MiniGameQuestionData>> onCompleteCallback, int testsCount)
        {
            _testsCount = testsCount;
            string characterName = PlayerPrefs.GetString("PickedPasteGameCharacter", string.Empty);
            _characterDescription = PasteGameCharactersDescriptionsFactory.GetDescription(characterName);
            StartCoroutine(GenerateCoroutine(onCompleteCallback));
        }

        private IEnumerator GenerateCoroutine(Action<List<MiniGameQuestionData>> onCompleteCallback)
        {
            var task = GenerateAsync();
            
            while (!task.IsCompleted)
            {
                yield return null;
            }
    
            if (task.Exception != null)
            {
                Debug.LogError("Error while generating paste game tests: " + task.Exception.Message);
                onCompleteCallback?.Invoke(new List<MiniGameQuestionData>());
            }
            else
            {
                onCompleteCallback?.Invoke(task.Result);
            }
        }

        private async Task<List<MiniGameQuestionData>> GenerateAsync()
        {
            try
            {
                var tests = new List<MiniGameQuestionData>();

                for (var i = 0; i < _testsCount; i++)
                {
                    var wordToPaste = _vocabulary.GetRandom().Original;
                    var sentence = await _sentenceGenerator.GenerateSentence(wordToPaste, _characterDescription);

                    // TODO: maybe add sentence validation
                    if (sentence.Contains(wordToPaste))
                    {
                        sentence = sentence.Replace(wordToPaste, "...");
                        tests.Add(new MiniGameQuestionData(sentence, new List<string>{wordToPaste}));
                    }
                    else
                    {
                        i--;
                    }
                }

                return tests;
            }
            catch (Exception e)
            {
                Debug.LogError("Error while generating paste game tests: " + e.Message);    
                return new List<MiniGameQuestionData>();
            }
        }
    }
}