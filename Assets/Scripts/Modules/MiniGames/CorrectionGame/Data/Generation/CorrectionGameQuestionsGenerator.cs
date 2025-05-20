using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Modules.MiniGames.Abstraction.Interfaces;
using Modules.MiniGames.Abstraction.Models;
using Modules.MiniGames.CorrectionGame.Data.Generation.TypoGenerators.Interfaces;
using Modules.VocabularyModule;
using Modules.VocabularyModule.Data.Models;
using UnityEngine;
using Zenject;

namespace Modules.MiniGames.CorrectionGame.Data.Generation
{
    public class CorrectionGameQuestionsGenerator : MonoBehaviour, IMiniGameQuestionsGenerator
    {
        private IAsyncTypoGenerator _asyncTypoGenerator;
        private Vocabulary _vocabulary;
        private int _testsCount;

        [Inject]
        private void Construct(IAsyncTypoGenerator asyncTypoGenerator, VocabularyController vocabularyController)
        {
            _asyncTypoGenerator = asyncTypoGenerator;
            _vocabulary = vocabularyController.Vocabulary;
        }
        
        public void Generate(Action<List<MiniGameQuestionData>> onCompleteCallback, int testsCount)
        {
            _testsCount = testsCount;
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
                Debug.LogError("Error while generating correction game tests: " + task.Exception.Message);
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
                    var word = _vocabulary.GetRandom().Original;
                    var wordWithTypo = await _asyncTypoGenerator.GenerateTypo(word);
                    tests.Add(new MiniGameQuestionData(wordWithTypo, new List<string> {word}));
                }

                return tests;
            }
            catch (Exception e)
            {
                Debug.LogError("Error while generating correction game tests: " + e.Message);
                return new List<MiniGameQuestionData>();
            }
        }
    }
}