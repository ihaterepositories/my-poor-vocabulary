using System.Collections.Generic;

namespace VocabularyModule.Data.Models
{
    public class Vocabulary
    {
        public List<Word> Words { get; set; }
        
        public Vocabulary()
        {
            Words = new List<Word>();
        }
    }
}