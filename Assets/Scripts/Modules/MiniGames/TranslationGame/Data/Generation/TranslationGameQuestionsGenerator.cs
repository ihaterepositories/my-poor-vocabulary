using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constants;
using Modules.MiniGames.Abstraction.Interfaces;
using Modules.MiniGames.Abstraction.Models;
using Modules.PersonalVocabulary;
using Modules.PersonalVocabulary.Data.Models;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace Modules.MiniGames.TranslationGame.Data.Generation
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
            if (testsCount % 3 != 0)
            {
                Debug.LogError("Translation game tests count must be divisible by 3");
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
                var words =  _vocabulary.GetWordsCount() >= AppConstants.WordsCountToUnlockMiniGames ? InitializeWordsList() : InitializeRandomWordsList();

                var tests = new List<MiniGameQuestionData>();
                Random rnd = new Random();

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
                            words[i].Translations[rnd.Next(0, words[i].Translations.Count)],
                            new List<string> { words[i].Original });
                    }
            
                    tests.Add(gameQuestion);
                }

                return ShuffleList(tests);
            });
        }

        private List<Word> InitializeRandomWordsList()
        {
            var randomWords = new List<Word>();
            for (int i = 0; i < _testsCount; i++)
            {
                randomWords.Add(_vocabulary.GetRandom());
            }
            return randomWords;
        }

        private List<Word> InitializeWordsList()
        {
            var newestWords = _vocabulary
                .GetSortedByNewest()
                .Take(_testsCount/3)
                .ToList();
            
            var randomWords = new List<Word>();
            for (int i = 0; i < _testsCount/3; i++)
            {
                randomWords.Add(_vocabulary.GetRandom());
            }
            
            var incorrectAnsweredWords = _vocabulary
                .GetProblematicWords()
                .ToList();
            
            // add some random words if there are not enough incorrect answered words
            if (incorrectAnsweredWords.Count < _testsCount/3)
            {
                var missingWordsCount = _testsCount/3 - incorrectAnsweredWords.Count;
                for (int i = 0; i < missingWordsCount; i++)
                {
                    incorrectAnsweredWords.Add(_vocabulary.GetRandom());
                }
            }
            // take only needed amount of incorrect answered words if there are too many of them
            else
            {
                incorrectAnsweredWords = incorrectAnsweredWords.Take(_testsCount/3).ToList();
            }
        
            return newestWords.Concat(randomWords).Concat(incorrectAnsweredWords).ToList();
        }
        
        private List<T> ShuffleList<T>(List<T> list)
        {
            Random rnd = new Random();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
            return list;
        }
    }
}