using System;
using UserInterface.Functional;

namespace Modules.MiniGames.PasteGame.UserInterface
{
    public class PasteGameCharacterToPlayerPrefsButton : ToPlayerPrefsSaverButtonBase
    {
        public static event Action OnButtonClicked;

        protected override void OnDataSaved()
        {
            OnButtonClicked?.Invoke();
        }
    }
}