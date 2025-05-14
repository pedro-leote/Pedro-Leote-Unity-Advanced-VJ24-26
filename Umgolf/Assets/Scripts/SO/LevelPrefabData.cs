using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelPrefabData
{
     //Transform Component
     public string _name;
     public Vector3 _position;
     public Quaternion _rotation;
     public Vector3 _scale;
     //Prefab Data
     public GameObject _prefab;
     public string _prefabName;
}
