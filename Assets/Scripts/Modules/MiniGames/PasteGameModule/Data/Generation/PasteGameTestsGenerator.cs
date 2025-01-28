using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Modules.MiniGames.PasteGameModule.Data.Generation.SentenceGenerators.Interfaces;
using Modules.MiniGames.PasteGameModule.Data.Models;
using Modules.VocabularyModule.Data.Models;
using UnityEngine;

namespace Modules.MiniGames.PasteGameModule.Data.Generation
{
    public class PasteGameTestsGenerator
    {
        private readonly IAsyncSentenceGenerator _sentenceGenerator;
        private readonly Vocabulary _vocabulary;
        private readonly int _testsPerGame;
        
        public PasteGameTestsGenerator(IAsyncSentenceGenerator asyncSentenceGenerator, Vocabulary vocabulary, int testsPerGame)
        {
            _vocabulary = vocabulary;
            _sentenceGenerator = asyncSentenceGenerator;
            _testsPerGame = testsPerGame;
        }
        
        public List<PasteGameTestData> Generate()
        {
            return Task.Run(async () => await GenerateAsync()).Result;
        }

        private async Task<List<PasteGameTestData>> GenerateAsync()
        {
            try
            {
                var tests = new List<PasteGameTestData>();

                for (var i = 0; i < _testsPerGame; i++)
                {
                    var wordToPaste = _vocabulary.GetRandom().Original;
                    var sentence = await _sentenceGenerator.GenerateSentenceWithWord(wordToPaste);
                    sentence = sentence.Replace(wordToPaste, "...");
                    tests.Add(new PasteGameTestData(wordToPaste, sentence));
                }

                return tests;
            }
            catch (Exception e)
            {
                Debug.LogError("Error while generating paste game tests: " + e.Message);
                return new List<PasteGameTestData>();
            }
        }
    }
}