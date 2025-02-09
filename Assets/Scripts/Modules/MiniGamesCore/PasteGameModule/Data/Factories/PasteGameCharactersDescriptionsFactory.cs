namespace Modules.MiniGamesCore.PasteGameModule.Data.Factories
{
    public static class PasteGameCharactersDescriptionsFactory
    {
        public static string GetDescription(string characterName)
        {
            switch (characterName)
            {
                case "Medieval knight": return "Medieval knight. Speaks loftily, using archaic words and constructions.";
                case "Scientist-linguist": return "Scientist-linguist. Formal and complex constructions, scientific style.";
                case "Pirate": return "Pirate. Informal, rough speech with maritime slang.";
                case "Robot from the future": return "Robot from the future. Structured, logical speech, possible technical terms.";
                case "Noir detective": return "Noir detective. Dramatic, metaphorical style, lots of descriptions.";
                case "Emotional teenager": return "Emotional teenager. Slang, simple sentences, possible grammatical errors.";
                case "Old wise man": return "Old wise man. A calm tone, phrases in the style of parables or aphorisms.";
                case "Cowboy": return "Cowboy. A simple, colloquial style with an American Western flavor.";
                case "Fantasy wizard": return "Fantasy wizard. A pompous style, mystical expressions.";
                case "An actor from a Shakespearean play": return "An actor from a Shakespearean play. Rhyme, poetic phrases.";
                default:
                    return "Medieval knight. Speaks loftily, using archaic words and constructions.";
            }
        }
    }
}