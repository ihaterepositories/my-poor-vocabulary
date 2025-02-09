using System.Collections;
using System.Threading.Tasks;
using Constants;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UserInterface.Functional.Navigation.Effects;
using Zenject;

namespace UserInterface.Functional.Navigation
{
    public class SceneLoader
    {
        private FogEffect _fogEffect;
        
        [Inject]
        private void Construct(FogEffect fogEffect)
        {
            _fogEffect = fogEffect;
        }
        
        public IEnumerator LoadSceneCoroutine(string sceneAddress)
        {
            _fogEffect.Increase(AppConstants.SceneLoadDelay);
            yield return new WaitForSeconds(AppConstants.SceneLoadDelay);
            yield return LoadScene(sceneAddress);
        }
        
        private async Task LoadScene(string sceneAddress)
        {
            var handle = Addressables.LoadSceneAsync(sceneAddress);
            await handle.Task;
            var loadedScene = handle.Result.Scene;
            SceneManager.SetActiveScene(loadedScene);
            _fogEffect.Decrease(AppConstants.SceneLoadDelay);
            UnloadUnusedScenes();
        }
        
        public IEnumerator LoadMenuCoroutine()
        {
            _fogEffect.Increase(AppConstants.SceneLoadDelay);
            yield return new WaitForSeconds(AppConstants.SceneLoadDelay);
            SceneManager.LoadScene("MenuScene");
            _fogEffect.Decrease(AppConstants.SceneLoadDelay);
            UnloadUnusedScenes();
        }
        
        private void UnloadUnusedScenes()
        {
            for (var i = SceneManager.sceneCount - 1; i >= 0; i--)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.isLoaded && scene != SceneManager.GetActiveScene())
                {
                    SceneManager.UnloadSceneAsync(scene);
                }
            }
        }
    }
}