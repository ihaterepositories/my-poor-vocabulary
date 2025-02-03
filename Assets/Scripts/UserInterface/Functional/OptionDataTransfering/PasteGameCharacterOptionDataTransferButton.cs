using System;

namespace UserInterface.Functional.OptionDataTransfering
{
    public class PasteGameCharacterOptionDataTransferButton : OptionDataTransferButtonBase
    {
        public static event Action OnButtonClicked;

        protected override void InvokeEvent()
        {
            OnButtonClicked?.Invoke();
        }
    }
}