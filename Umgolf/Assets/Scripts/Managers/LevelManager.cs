using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEditor;
using UnityEditor.VersionControl;

public class LevelManager : MonoBehaviour
{
    //The general gist is to, in demand, unload & load new terrain layouts, thus "changing the level" without touching the Scene.
    //This makes it easier to handle new levels as merely new SO configurations, decreases loading periods, and works with less objects over the runtime.
    [SerializeField] private LevelLayout _currentLevelLayout;
    [SerializeField] private LevelLayout _nextLevelLayout;

    private bool _canFunction = true;
    //Não sei ainda usar Dictionaries neste case, porque precisava de dar check
    private Dictionary<int, LevelLayout> _levelLayoutDictionary = new Dictionary<int, LevelLayout>();
    
    
    #if UNITY_EDITOR
    private void Update()
    {
        
    }
    #endif
    
    
    //This is the first iteration, continuosly triggering and checking files when we need a new one. I'm not happy with it
    public GameObject InitializeLevelData(int levelIndex)
    {
        //Run file getter
        
        
        return null;
    }

    public void UnloadLevelData(LevelLayout levelLayout)
    {
        
    }
    
}
