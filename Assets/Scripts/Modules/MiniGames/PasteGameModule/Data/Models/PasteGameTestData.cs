namespace Modules.MiniGames.PasteGameModule.Data.Models
{
    public class PasteGameTestData
    {
        public string WordToPaste { get; set; }
        public string Sentence { get; set; }
        
        public PasteGameTestData(string wordToPaste, string sentence)
        {
            WordToPaste = wordToPaste;
            Sentence = sentence;
        }
    }
}