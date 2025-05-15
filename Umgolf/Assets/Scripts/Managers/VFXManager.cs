using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject _bounceVFX;

    private List<GameObject> _bounceVFXPool = new List<GameObject>();
    [SerializeField] private int _bouncePoolSize = 4;
    private static VFXManager _instance;
    public static VFXManager Instance
    {
    	get
    	{
    		if (_instance == null)
    		{
    			_instance = new VFXManager();
    		}
    
    		return _instance;
    	}
    }

    private void Start()
    {
        for (int i = 0; i < _bouncePoolSize; ++i)
        {
            GameObject poolObject = Instantiate(_bounceVFX, transform);
            _bounceVFXPool.Add(poolObject);
            _bounceVFXPool[i].SetActive(false);
        }
    }

    private GameObject GetBounceVFX()
    {
        for (int i = 0; i < _bounceVFXPool.Count; ++i)
        {
            if (!_bounceVFXPool[i].activeInHierarchy)
            {
                return _bounceVFXPool[i];
            }
        }
        return null;
    }


    public void CreateBounceVFX(Vector2 collisionPoint)
    {
        StartCoroutine(BounceVFX(collisionPoint));
    }

    public IEnumerator BounceVFX(Vector2 collisionPoint)
    {
        GameObject bounceVFX = GetBounceVFX();
        bounceVFX.transform.position = new Vector3(collisionPoint.x, collisionPoint.y, 0);
        bounceVFX?.SetActive(true);
        bounceVFX.GetComponent<ParticleSystem>()?.Play();
        yield return new WaitForSeconds(0.35f);
        bounceVFX?.SetActive(false);
    }
}
