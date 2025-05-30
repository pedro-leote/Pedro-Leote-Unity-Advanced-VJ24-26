
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelManager : MonoSingleton<LevelManager>
{
    //The general gist is to, in demand, unload & load new terrain layouts, thus "changing the level" without touching the Scene.
    //This makes it easier to handle new levels as merely new SO configurations, decreases loading periods, and works with less objects over the runtime.
    [SerializeField] private LevelLayout _currentLevelLayout;
    [SerializeField] private LevelLayout _nextLevelLayout;
    
    [SerializeField] private SerializedDictionary<int, LevelLayout> _levelLayoutDictionary = new SerializedDictionary<int, LevelLayout>();
    
    private List<GameObject> _genericObjectList = new List<GameObject>();
    
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
            return null;
            
        }

        GameObject levelObject = StartLoading(_levelLayoutDictionary[levelIndex]);

        return levelObject; // Temp
    }

    public void UnloadLevelData(GameObject levelObject)
    {
        //TODO: Detect poolable entries and send them back to usable state
        //actually. why do I need this
    }

    private GameObject StartLoading(LevelLayout levelLayout)
    {
        GameObject instantiatedParent = new GameObject();
        instantiatedParent.name = levelLayout._levelParentObject._name;
        
        //Grab object pool from Level Builder
        _genericObjectList = LevelBuilder.Instance.GrabPool();
        
        for (int i = 0; i < levelLayout._levelObjects.Count; ++i)
        {
            _genericObjectList[i].transform.position = levelLayout._levelObjects[i]._position;
            _genericObjectList[i].transform.rotation = levelLayout._levelObjects[i]._rotation;
            _genericObjectList[i].transform.localScale = levelLayout._levelObjects[i]._scale;
            
            _genericObjectList[i].GetComponent<SpriteRenderer>().color = levelLayout._levelObjects[i]._spriteRendererColor;
            _genericObjectList[i].GetComponent<SpriteRenderer>().sortingOrder = levelLayout._levelObjects[i]._spriteRendererLayer;
            
            _genericObjectList[i].GetComponent<BoxCollider2D>().enabled = levelLayout._levelObjects[i]._collider2DState;
            _genericObjectList[i].GetComponent<BoxCollider2D>().size = levelLayout._levelObjects[i]._collider2DSize;
            
            _genericObjectList[i].transform.parent = instantiatedParent.transform;
            
        }

        for (int i = 0; i < levelLayout._levelPrefabs.Count; ++i)
        {
            GameObject childPrefabObject = Instantiate(levelLayout._levelPrefabs[i]._prefab);
            childPrefabObject.transform.position = levelLayout._levelPrefabs[i]._position;
            childPrefabObject.transform.rotation = levelLayout._levelPrefabs[i]._rotation;
            childPrefabObject.transform.localScale = levelLayout._levelPrefabs[i]._scale;
            
            childPrefabObject.transform.parent = instantiatedParent.transform;
        }
        
        return instantiatedParent;
    }
    
}
