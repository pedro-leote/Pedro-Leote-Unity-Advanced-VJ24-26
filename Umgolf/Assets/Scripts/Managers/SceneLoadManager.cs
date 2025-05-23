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

            sceneOperation.allowSceneActivation = true;
            return;
        }
        
        SceneManager.LoadScene(sceneName);
    }

    public void SwitchToScene(string sceneName, float delay)
    {
        if (_sceneOperations.TryGetValue(sceneName, out AsyncOperation sceneOperation))
        {
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
