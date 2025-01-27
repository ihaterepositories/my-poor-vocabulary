namespace Modules.MiniGames.CorrectionGameModule.Data.Models
{
    public class CorrectionGameTestData
    {
        public string OriginalWord { get; set; }
        public string TypoWord { get; set; }
        
        public CorrectionGameTestData(string originalWord, string typoWord)
        {
            OriginalWord = originalWord;
            TypoWord = typoWord;
        }
    }
}