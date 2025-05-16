using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


//Used in the Loading screen to initialize everything into who needs it: levels, save file, next 2 scenes.
public class GameLoader : MonoBehaviour
{
    private LevelLayout[] _levelLayoutArray = null;

    private bool _hasFinishedSaveLoad = false;
    private bool _hasFinishedLevels = false;
    private bool _hasFinishedScenes = false;
    
    public UnityEvent<LevelLayout[]> OnLevelsLoadedEvent = new UnityEvent<LevelLayout[]>();
    public UnityEvent OnScenesLoadedEvent = new UnityEvent();
    
    void Start()
    { 

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
        if (foundLayouts == null)
        {
            yield return false;
        }
        
        for (int i = 0; i < foundLayouts.Length; ++i)
        {
            ResourceRequest request = Resources.LoadAsync<LevelLayout>(foundLayouts[i].name);
            yield return request; 
            _levelLayoutArray[i] = request.asset as LevelLayout;
        }
        yield return true;
        _hasFinishedLevels = true;
    }
    

    private IEnumerator LoadSave()
    {
        SaveManager.Instance.LoadGameState();
        yield return new WaitForEndOfFrame();
        _hasFinishedSaveLoad = true;
    }
    
    private IEnumerator LoadScenes()
    {
        yield return SceneLoadManager.Instance.LoadScenes();
        _hasFinishedScenes = true;
    }
}
