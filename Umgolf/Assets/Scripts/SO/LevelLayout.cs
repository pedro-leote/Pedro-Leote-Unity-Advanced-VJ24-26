using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Layout", menuName = "Level Layout")]
public class LevelLayout : ScriptableObject
{
    public int _levelIndex;
    
    public Vector3 _ballPosition;
    public Vector3 _holePosition;

    public LevelObjectData _levelParentObject;
    public List<LevelObjectData> _levelObjects = new List<LevelObjectData>();
    public List<LevelPrefabData> _levelPrefabs = new List<LevelPrefabData>();
    
}
