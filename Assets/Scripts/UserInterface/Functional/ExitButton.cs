using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Functional
{
    public class ExitButton : MonoBehaviour
    {
        [SerializeField] private Button exitButton;
        
        private void Start()
        {
            exitButton.onClick.AddListener(Exit);
        }
        
        private void Exit()
        {
            Application.Quit();
        }
    }
}