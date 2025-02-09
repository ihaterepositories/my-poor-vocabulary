using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Modules.MiniGamesCore.Abstraction.Interfaces;
using Modules.MiniGamesCore.Abstraction.Models;
using Modules.VocabularyModule;
using Modules.VocabularyModule.Data.Models;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Modules.MiniGamesCore.TranslationGameModule.Data.Generation
{
    // TODO: refactor
    public class TranslationGameQuestionsGenerator : MonoBehaviour, IMiniGameQuestionsGenerator
    {
        private Vocabulary _vocabulary;
        private int _testsCount;

        [Inject]
        private void Construct(VocabularyController vocabularyController)
        {
            _vocabulary = vocabularyController.Vocabulary;
        }
        
        public void Generate(Action<List<MiniGameQuestionData>> onCompleteCallback, int testsCount)
        {
            if (testsCount % 3 != 0 || testsCount < 30)
            {
                Debug.LogError("Translation game tests count must be divisible by 3 and at least 30");
                return;
            }
            
            if (_vocabulary.GetCount() < 50)
            {
                Debug.LogError("Vocabulary must contain at least 50 words to generate tests");
                return;
            }
            
            _testsCount = testsCount;

            StartCoroutine(GenerateCoroutine(onCompleteCallback));
        }
        
        private IEnumerator GenerateCoroutine(Action<List<MiniGameQuestionData>> onCompleteCallback)
        {
            var task = GenerateAsync();
            
            yield return new WaitUntil(() => task.IsCompleted);

            if (task.Exception != null)
            {
                Debug.LogError("Error while generating translation game tests: " + task.Exception.Message);
                onCompleteCallback?.Invoke(new List<MiniGameQuestionData>());
            }
            else
            {
                onCompleteCallback?.Invoke(task.Result);
            }
        }


        private async Task<List<MiniGameQuestionData>> GenerateAsync()
        {
            return await Task.Run(() =>
            {
                var words = InitializeWordsList();
                var tests = new List<MiniGameQuestionData>();

                for (int i = 0; i < words.Count; i++)
                {
                    MiniGameQuestionData gameQuestion;
            
                    if (i % 2 == 0)
                    {
                        gameQuestion = new MiniGameQuestionData(words[i].Original, words[i].Translations);
                    }
                    else
                    {
                        gameQuestion = new MiniGameQuestionData(
                            words[i].Translations[Random.Range(0, words[i].Translations.Count)],
                            new List<string> { words[i].Original });
                    }
            
                    tests.Add(gameQuestion);
                }

                return ShuffleList(tests);
            });
        }

        private List<Word> InitializeWordsList()
        {
            var randomWords = new List<Word>();
            for (int i = 0; i < _testsCount; i++)
            {
                randomWords.Add(_vocabulary.GetRandom());
            }
            return randomWords;
        }

        // private List<Word> InitializeWordsList()
        // {
        //     var newestWords = _vocabulary
        //         .GetSortedByNewest()
        //         .Take(_testsCount/3)
        //         .ToList();
        //     
        //     var randomWords = new List<Word>();
        //     for (int i = 0; i < _testsCount/3; i++)
        //     {
        //         randomWords.Add(_vocabulary.GetRandom());
        //     }
        //     
        //     var incorrectAnsweredWords = _vocabulary
        //         .GetIncorrectTranslatedInTranslationGame()
        //         .ToList();
        //     
        //     // add some random words if there are not enough incorrect answered words
        //     if (incorrectAnsweredWords.Count < _testsCount/3)
        //     {
        //         var missingWordsCount = _testsCount/3 - incorrectAnsweredWords.Count;
        //         for (int i = 0; i < missingWordsCount; i++)
        //         {
        //             incorrectAnsweredWords.Add(_vocabulary.GetRandom());
        //         }
        //     }
        //     // take only needed amount of incorrect answered words if there are too many of them
        //     else
        //     {
        //         incorrectAnsweredWords = incorrectAnsweredWords.Take(_testsCount/3).ToList();
        //     }
        //
        //     return newestWords.Concat(randomWords).Concat(incorrectAnsweredWords).ToList();
        // }
        
        private List<T> ShuffleList<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
            return list;
        }
        
        // private List<string> FindSimilarStringsTo(string input, List<string> words)
        // {
        //     return words
        //         .Select(word => new { Word = word, Distance = LevenshteinDistance(input, word) })
        //         .OrderBy(pair => pair.Distance)
        //         .Take(10)
        //         .Select(pair => pair.Word)
        //         .ToList();
        // }
        //
        // private int LevenshteinDistance(string source, string target)
        // {
        //     if (source == null) return target?.Length ?? 0;
        //     if (target == null) return source.Length;
        //
        //     int m = source.Length;
        //     int n = target.Length;
        //     var dp = new int[m + 1, n + 1];
        //
        //     for (int i = 0; i <= m; i++) dp[i, 0] = i;
        //     for (int j = 0; j <= n; j++) dp[0, j] = j;
        //
        //     for (int i = 1; i <= m; i++)
        //     {
        //         for (int j = 1; j <= n; j++)
        //         {
        //             int cost = (source[i - 1] == target[j - 1]) ? 0 : 1;
        //             dp[i, j] = Math.Min(
        //                 Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1),
        //                 dp[i - 1, j - 1] + cost
        //             );
        //         }
        //     }
        //
        //     return dp[m, n];
        // }
    }
}