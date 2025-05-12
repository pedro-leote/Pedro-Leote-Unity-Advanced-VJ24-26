using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //The general gist is to, in demand, unload & load new terrain layouts, thus "changing the level" without touching the Scene.
    //This makes it easier to handle new levels as merely new SO configurations, decreases loading periods, and works with less objects over the runtime.
    [SerializeField] private LevelLayout _currentLevelLayout;
    [SerializeField] private LevelLayout _nextLevelLayout;

    public GameObject InitializeLevelData(LevelLayout levelLayout, Transform positionToPlaceAt)
    {
        GameObject parentObject = levelLayout._levelParentObject;


        return null; // Replace
    }
    
}
