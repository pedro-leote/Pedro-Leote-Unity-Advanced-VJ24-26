using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoSingleton<SceneLoadManager>
{
    private Dictionary<string, AsyncOperation> _sceneOperations = new Dictionary<string, AsyncOperation>();
    
    public IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation sceneOperation = SceneManager.LoadSceneAsync(sceneName);
        sceneOperation.allowSceneActivation = false;
        _sceneOperations.Add(sceneName, sceneOperation);
        yield return null;
    }

    public void SwitchToScene(string sceneName)
    {
        if (_sceneOperations.TryGetValue(sceneName, out AsyncOperation sceneOperation))
        {
            Debug.Log($"Switching to scene {sceneName}");
            sceneOperation.allowSceneActivation = true;
            return;
        }
        
        Debug.Log($"Could not find scene {sceneName}. Loading now...");
        SceneManager.LoadScene(sceneName);
    }

    public void SwitchToScene(string sceneName, float delay)
    {
        if (_sceneOperations.TryGetValue(sceneName, out AsyncOperation sceneOperation))
        {
            Debug.Log($"Switching to scene {sceneName}");
            StartCoroutine(SceneLoaderDelay(sceneOperation, delay));
            return;
        }
    }

    private IEnumerator SceneLoaderDelay(AsyncOperation scene, float delay)
    {
        yield return new WaitForSeconds(delay);
        scene.allowSceneActivation = true;
    }
}
