namespace CorrectionGameModule.Models
{
    public class TestData
    {
        public string OriginalWord { get; set; }
        public string TypoWord { get; set; }
        
        public TestData(string originalWord, string typoWord)
        {
            OriginalWord = originalWord;
            TypoWord = typoWord;
        }
    }
}