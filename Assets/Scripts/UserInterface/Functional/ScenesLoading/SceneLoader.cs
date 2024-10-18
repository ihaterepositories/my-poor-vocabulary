using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace UserInterface.Functional.ScenesLoading
{
    public class SceneLoader
    {
        public IEnumerator LoadSceneWithDelayCoroutine(string sceneAddress, float delay)
        {
            yield return new WaitForSeconds(delay);
            LoadScene(sceneAddress);
        }
        
        private async void LoadScene(string sceneAddress)
        {
            UnloadUnusedScenes();
            
            var handle = Addressables.LoadSceneAsync(sceneAddress);
            await handle.Task;
            var loadedScene = handle.Result.Scene;
            
            SceneManager.SetActiveScene(loadedScene);
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