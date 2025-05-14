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

    public LevelObjectData _levelParentObject;
    public List<LevelObjectData> _levelObjects = new List<LevelObjectData>();
    public List<GameObject> _levelPrefabs = new List<GameObject>();
    
    //TODO: Integrate this class as the information of each given GameObject. Have a Serializer and Deserializer class, and parse this class through the tool class
}
