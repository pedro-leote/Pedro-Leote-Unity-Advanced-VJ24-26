using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Layout", menuName = "Level Layout")]
public class LevelLayout : ScriptableObject
{
    public int _levelIndex;
    
    public Transform _ballPosition;
    public Transform _holePosition;

    public GameObject _levelParentObject;
    public List<GameObject> _levelObjects = new List<GameObject>();
    
    public class LevelObjectData
    {
        
    }
}
