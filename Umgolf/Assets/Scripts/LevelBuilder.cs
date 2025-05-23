using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoSingleton<LevelBuilder>
{
    [SerializeField] private int _genericPoolSize;
    [SerializeField] private List<GameObject> _genericPool = new List<GameObject>();

    [SerializeField] private Sprite _genericObjSprite;
    [SerializeField] private Material _genericObjMaterial;
    // Start is called before the first frame update
    
    void Start()
    {
        CreateGenericPool();
    }

    public void CreateGenericPool()
    {
        for (int i = 0; i < _genericPoolSize; ++i)
        {
            GameObject genericObj = new GameObject();
            genericObj.transform.position = Instance.transform.position;
            genericObj.transform.parent = Instance.transform;

            genericObj.AddComponent<SpriteRenderer>();
            genericObj.GetComponent<SpriteRenderer>().sprite = _genericObjSprite;
            genericObj.GetComponent<SpriteRenderer>().material = _genericObjMaterial;
            
            genericObj.AddComponent<BoxCollider2D>();
            _genericPool.Add(genericObj);
            
        }
    }


    public List<GameObject> GrabPool()
    {
        if (_genericPool.Count == 0)
        {
            CreateGenericPool();
        }
        
        return _genericPool;
    }
}
