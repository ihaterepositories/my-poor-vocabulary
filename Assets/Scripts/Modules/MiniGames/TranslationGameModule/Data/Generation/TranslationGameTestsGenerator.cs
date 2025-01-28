using System;
using System.Collections.Generic;
using System.Linq;
using Modules.MiniGames.TranslationGameModule.Data.Models;
using Modules.VocabularyModule;
using Modules.VocabularyModule.Data.Models;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace Modules.MiniGames.TranslationGameModule.Data.Generation
{
    // TODO: refactor
    public class TranslationGameTestsGenerator
    {
        private Vocabulary _vocabulary;
        private readonly int _testsPerGame;
        
        public TranslationGameTestsGenerator(int testsPerGame)
        {
            if (_testsPerGame % 3 != 0)
            {
                Debug.LogError("Translation game tests count must be divisible by 3");
            }
            
            _testsPerGame = testsPerGame;
        }
        
        [Inject]
        private void Construct(VocabularyController vocabularyController)
        {
            _vocabulary = vocabularyController.Vocabulary;
        }

        public List<TranslationGameTestData> Generate()
        {
            var words = InitializeWordsList();
            var tests = new List<TranslationGameTestData>();

            for (int i = 0; i < words.Count; i++)
            {
                TranslationGameTestData gameTest;
                
                if (i % 2 == 0)
                {
                    gameTest = new TranslationGameTestData()
                    {
                        WordToTranslate = words[i].Original,
                        CorrectAnswer = words[i].Translation,
                        PossibleAnswers = ShuffleList(FindSimilarStringsTo(words[i].Translation, _vocabulary.GetAllTranslations()))
                    };
                }
                else
                {
                    gameTest = new TranslationGameTestData()
                    {
                        WordToTranslate = words[i].Translation,
                        CorrectAnswer = words[i].Original,
                        PossibleAnswers = ShuffleList(FindSimilarStringsTo(words[i].Original, _vocabulary.GetAllOriginals()))
                    };
                }
                
                tests.Add(gameTest);
            }
            
            return ShuffleList(tests);
        } 
        
        private List<Word> InitializeWordsList()
        {
            var newestWords = _vocabulary
                .GetSortedByNewest()
                .Take(_testsPerGame/3)
                .ToList();
            
            var randomWords = new List<Word>();
            for (int i = 0; i < _testsPerGame/3; i++)
            {
                randomWords.Add(_vocabulary.GetRandom());
            }
            
            var incorrectAnsweredWords = _vocabulary
                .GetIncorrectTranslatedInTranslationGame()
                .Take(_testsPerGame/3)
                .ToList();

            return newestWords.Concat(randomWords).Concat(incorrectAnsweredWords).ToList();
        }
        
        private List<string> FindSimilarStringsTo(string input, List<string> words)
        {
            return words
                .Select(word => new { Word = word, Distance = LevenshteinDistance(input, word) })
                .OrderBy(pair => pair.Distance)
                .Take(10)
                .Select(pair => pair.Word)
                .ToList();
        }
        
        private int LevenshteinDistance(string source, string target)
        {
            if (source == null) return target?.Length ?? 0;
            if (target == null) return source.Length;

            int m = source.Length;
            int n = target.Length;
            var dp = new int[m + 1, n + 1];

            for (int i = 0; i <= m; i++) dp[i, 0] = i;
            for (int j = 0; j <= n; j++) dp[0, j] = j;

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    int cost = (source[i - 1] == target[j - 1]) ? 0 : 1;
                    dp[i, j] = Math.Min(
                        Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1),
                        dp[i - 1, j - 1] + cost
                    );
                }
            }

            return dp[m, n];
        }
        
        private List<T> ShuffleList<T>(List<T> list)
        {
            var random = new Random();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
            return list;
        }
    }
}