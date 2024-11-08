using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.General.Navigation
{
    public class SceneLoadProvider : MonoBehaviour
    {
        [SerializeField] private Button uiButton;
        [SerializeField] private bool isGoBackButton;
        [SerializeField] private KeyCode hotkey;
        [SerializeField] private string sceneAddress;
        
        private SceneLoader _sceneLoader;
        
        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Awake()
        {
            if (isGoBackButton)
            {
                uiButton.onClick.AddListener(LoadMenu);
                return;
            }
            
            uiButton.onClick.AddListener(LoadScene);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(hotkey))
            {
                LoadScene();
            }
        }
        
        private void LoadScene()
        {
            StartCoroutine(_sceneLoader.LoadSceneCoroutine(sceneAddress));
        }
        
        private void LoadMenu()
        {
            StartCoroutine(_sceneLoader.LoadMenuCoroutine());
        }
    }
}