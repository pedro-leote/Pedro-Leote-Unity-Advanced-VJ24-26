using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine.Rendering;

public class LevelManager : MonoSingleton<LevelManager>
{
    //The general gist is to, in demand, unload & load new terrain layouts, thus "changing the level" without touching the Scene.
    //This makes it easier to handle new levels as merely new SO configurations, decreases loading periods, and works with less objects over the runtime.
    [SerializeField] private LevelLayout _currentLevelLayout;
    [SerializeField] private LevelLayout _nextLevelLayout;
    
    [SerializeField] private SerializedDictionary<int, LevelLayout> _levelLayoutDictionary = new SerializedDictionary<int, LevelLayout>();
    
    public void ReceiveDictionary(LevelLayout[] levelLayoutCollection)
    {
        for (int i = 0; i < levelLayoutCollection.Length; ++i)
        {
            _levelLayoutDictionary.Add(levelLayoutCollection[i]._levelIndex, levelLayoutCollection[i]);
        }
    }

    public void CleanDictionary()
    {
        _levelLayoutDictionary.Clear();
    }
    

    public GameObject GrabLevelData(int levelIndex)
    {
        if (!_levelLayoutDictionary.ContainsKey(levelIndex))
        {
            Debug.Log("Could not find given level index. Quitting.");
            return null;
            
        }

        GameObject levelObject = StartLoading(_levelLayoutDictionary[levelIndex]);

        return levelObject; // Temp
    }

    public void UnloadLevelData(GameObject levelObject)
    {
        //TODO: Detect poolable entries and send them back to usable state
        
    }

    private GameObject StartLoading(LevelLayout levelLayout)
    {
        GameObject _instantiatedParent = new GameObject();
                
        
        return null; // Temp
    }
    
}
