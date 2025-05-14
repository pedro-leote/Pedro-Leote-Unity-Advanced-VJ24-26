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
    
    
    //TODO: Integrate this class as the information of each given GameObject. Have a Serializer and Deserializer class, and parse this class through the tool class
    [Serializable]
    public class LevelObjectData
    {
        [SerializeField] private Vector3 _position;
        [SerializeField] private Quaternion _rotation;
        [SerializeField] private Vector3 _scale;
        [SerializeField] private Collider2D _boxCollider;
        [SerializeField] private SpriteRenderer _spriteRenderer;
    }
}
