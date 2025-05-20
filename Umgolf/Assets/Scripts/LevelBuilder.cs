using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private int _genericPoolSize;
    [SerializeField] private List<GameObject> _genericPool = new List<GameObject>();

    [SerializeField] private Sprite _genericObjSprite;
    [SerializeField] private Material _genericObjMaterial;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {
        CreateGenericPool();

    }

    public void CreateGenericPool()
    {
        for (int i = 0; i < _genericPoolSize; ++i)
        {
            GameObject genericObj = new GameObject();
            genericObj.transform.position = this.transform.position;
            genericObj.transform.parent = this.transform;

            genericObj.AddComponent<SpriteRenderer>();
            genericObj.GetComponent<SpriteRenderer>().sprite = _genericObjSprite;
            genericObj.GetComponent<SpriteRenderer>().material = _genericObjMaterial;
            
            genericObj.AddComponent<BoxCollider2D>();
            genericObj.SetActive(false);
            
            _genericPool.Add(genericObj);
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        return;
    }

    public List<GameObject> GrabFromPool()
    {
        if (_genericPool.Count == 0)
        {
            Debug.Log("Did not find object pool. Creating...");
            CreateGenericPool();
            return _genericPool;
        }
        
        return _genericPool;
    }
}
