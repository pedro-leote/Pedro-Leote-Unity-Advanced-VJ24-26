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

    [Serializable] public class LevelObject
    {
        public GameObject _prefab;
        public Vector3 _position;
        public Quaternion _rotation;
        public Vector3 _scale;
    }
    public List<LevelObject> _levelObjects = new List<LevelObject>();
}
