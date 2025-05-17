using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


//Used in the Loading screen to initialize everything into who needs it: levels, save file, next 2 scenes.
public class GameLoader : MonoBehaviour
{

    [SerializeField] private bool _hasFinishedSaveLoad = false;
    [SerializeField] private bool _hasFinishedLevels = false;
    [SerializeField] private bool _hasFinishedScenes = false;
    
    public UnityEvent<LevelLayout[]> OnLevelsLoadedEvent = new UnityEvent<LevelLayout[]>();
    public UnityEvent OnScenesLoadedEvent = new UnityEvent();
    
    void Start()
    { 
        StartCoroutine(WaitUntilStartingLoading());
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasFinishedLevels && _hasFinishedScenes && _hasFinishedSaveLoad)
        {
            StopAllCoroutines();
            SceneLoadManager.Instance.SwitchToScene("TitleScreen");
        }
        
    }


    private IEnumerator WaitUntilStartingLoading()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(LoadSave());
        StartCoroutine(LoadLevels());
        StartCoroutine(LoadScenes());
        
        StopCoroutine(WaitUntilStartingLoading());
    }
    private IEnumerator LoadLevels()
    {
        LevelLayout[] foundLayouts = Resources.FindObjectsOfTypeAll<LevelLayout>();
        if (foundLayouts == null || foundLayouts.Length == 0)
        {
            yield break;
        }
        
        LevelLayout[] layoutArray = new LevelLayout[foundLayouts.Length];
        for (int i = 0; i < foundLayouts.Length; ++i)
        {
            ResourceRequest request = Resources.LoadAsync<LevelLayout>(foundLayouts[i].name);
            yield return request; 
            layoutArray[i] = request.asset as LevelLayout;
        }
        
        OnLevelsLoadedEvent?.Invoke(layoutArray);
        yield return true;
        _hasFinishedLevels = true;
    }
    

    private IEnumerator LoadSave()
    {

        if (File.Exists(Application.persistentDataPath + "/umgolfsave.json"))
        {
            yield return SaveManager.Instance.LoadGameState();
        }

        _hasFinishedSaveLoad = true;
    }
    
    private IEnumerator LoadScenes()
    {
        yield return SceneLoadManager.Instance.LoadScenes();
        OnScenesLoadedEvent?.Invoke();
        _hasFinishedScenes = true;
    }
}
