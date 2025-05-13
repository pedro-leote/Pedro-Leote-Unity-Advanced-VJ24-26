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
        //Obter o parent object do SO
        GameObject parentObject = levelLayout._levelParentObject;
        List<GameObject> childObjects = new List<GameObject>();
        //Obter todos os filhos desse parent //TODO: Alterar terreno e boundary items para um object pool, porque são quase sempre os mesmos
        for (int i = 0; i < parentObject.GetComponentInChildren<Transform>().childCount; ++i)
        {
            childObjects.Add(parentObject.GetComponentInChildren<Transform>().GetChild(i).gameObject);
        }
        //Instanciar os filhos nas suas posições, mover o parent para a posição onde é adequada.
        
        return parentObject;
    }

    public void UnloadLevelData(LevelLayout levelLayout)
    {
        
    }
    
}
