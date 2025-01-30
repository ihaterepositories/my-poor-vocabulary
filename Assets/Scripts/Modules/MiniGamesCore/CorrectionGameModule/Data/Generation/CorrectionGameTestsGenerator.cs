using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Modules.MiniGamesCore.CorrectionGameModule.Data.Generation.TypoGenerators.Interfaces;
using Modules.MiniGamesCore.CorrectionGameModule.Data.Models;
using Modules.VocabularyModule.Data.Models;
using UnityEngine;

namespace Modules.MiniGamesCore.CorrectionGameModule.Data.Generation
{
    public class CorrectionGameTestsGenerator
    {
        private readonly IAsyncTypoGenerator _asyncTypoGenerator;
        private readonly Vocabulary _vocabulary;
        private readonly int _testsPerGame;
        
        public CorrectionGameTestsGenerator(IAsyncTypoGenerator asyncTypoGenerator, Vocabulary vocabulary, int testsPerGame)
        {
            _asyncTypoGenerator = asyncTypoGenerator;
            _vocabulary = vocabulary;
            _testsPerGame = testsPerGame;
        }
        
        public List<CorrectionGameTestData> Generate()
        {
            return Task.Run(async () => await GenerateAsync()).Result;
        }
        
        private async Task<List<CorrectionGameTestData>> GenerateAsync()
        {
            try
            {
                var tests = new List<CorrectionGameTestData>();
            
                for (var i = 0; i < _testsPerGame; i++)
                {
                    var word = _vocabulary.GetRandom().Original;
                    var typo = await _asyncTypoGenerator.GenerateTypo(word);
                    tests.Add(new CorrectionGameTestData(word, typo));
                }

                return tests;
            }
            catch (Exception e)
            {
                Debug.LogError("Error while generating correction game tests: " + e.Message);
                return new List<CorrectionGameTestData>();
            }
        }
    }
}