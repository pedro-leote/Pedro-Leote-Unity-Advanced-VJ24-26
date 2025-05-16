using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private Dictionary<string, AsyncOperation> _sceneOperations = new Dictionary<string, AsyncOperation>();
    
    
    
    private static SceneLoadManager _instance;
    public static SceneLoadManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SceneLoadManager();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(Instance);
    }
    public IEnumerator LoadScenes()
    {
        //For Title Screen
        AsyncOperation titleOperation = SceneManager.LoadSceneAsync("TitleScreen");
        titleOperation.allowSceneActivation = false;
        _sceneOperations.Add("TitleScreen", titleOperation);
        //For Game screen
        AsyncOperation gameOperation = SceneManager.LoadSceneAsync("GameScreen");
        gameOperation.allowSceneActivation = false;
        _sceneOperations.Add("GameScreen", gameOperation);
        
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
