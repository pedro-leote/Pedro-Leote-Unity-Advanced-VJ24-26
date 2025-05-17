using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoSingleton<SceneLoadManager>
{
    private Dictionary<string, AsyncOperation> _sceneOperations = new Dictionary<string, AsyncOperation>();
    
    public IEnumerator LoadScenes()
    {
        //For Title Screen
        AsyncOperation titleOperation = SceneManager.LoadSceneAsync("TitleScreen");
        titleOperation.allowSceneActivation = false;
        while (!titleOperation.isDone)
        {
            yield return null;
        }
        _sceneOperations.Add("TitleScreen", titleOperation);
        //For Game screen
        AsyncOperation gameOperation = SceneManager.LoadSceneAsync("GameScene");
        gameOperation.allowSceneActivation = false;
        while (!gameOperation.isDone)
        {
            yield return null;
        }
        _sceneOperations.Add("GameScene", gameOperation);
        
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
    
}
