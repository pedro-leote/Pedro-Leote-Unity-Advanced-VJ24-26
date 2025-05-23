using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VFXManager : MonoSingleton<VFXManager>
{
    [SerializeField] private GameObject _bounceVFX;
    [SerializeField] private GameObject _coinVFX;
    
    private List<GameObject> _coinVFXPool = new List<GameObject>();
    private List<GameObject> _bounceVFXPool = new List<GameObject>();
    [SerializeField] private int _bouncePoolSize = 4;
    [SerializeField] private int _coinPoolSize = 3;

    public enum VFXType
    {
        Bounce,
        Coin
    }
    
    
    private void Start()
    {
        int enumEntries = VFXType.GetValues(typeof(VFXType)).Length;

        for (int i = 0; i < enumEntries; ++i)
        {
            InitializePool((VFXType)i);
        }
    }

    private void InitializePool(VFXType vfxType)
    {
        switch (vfxType)
        {
            case VFXType.Bounce:
                for (int i = 0; i < _bouncePoolSize; ++i)
                {
                    GameObject poolObject = Instantiate(_bounceVFX, transform);
                    _bounceVFXPool.Add(poolObject);
                    _bounceVFXPool[i].SetActive(false);
                }
                break;
            case VFXType.Coin:
                for (int i = 0; i < _coinPoolSize; ++i)
                {
                    GameObject poolObject = Instantiate(_coinVFX, transform);
                    _coinVFXPool.Add(poolObject);
                    _coinVFXPool[i].SetActive(false);
                }
                break;
        }
    }
    
    private GameObject GetVFXFromPool(VFXType type)
    {
        switch (type)
        {
            case VFXType.Bounce:
                for (int i = 0; i < _bounceVFXPool.Count; ++i)
                {
                    if (!_bounceVFXPool[i].activeInHierarchy)
                    {
                        return _bounceVFXPool[i];
                    }
                }
                break;
            case VFXType.Coin:
                for (int i = 0; i < _coinVFXPool.Count; ++i)
                {
                    if (!_coinVFXPool[i].activeInHierarchy)
                    {
                        return _coinVFXPool[i];
                    }
                }
                break;
        }
        return null;
    }
    
    //I got lazy & didnt wanna delve into multiple arg assigment no Unity (pelo que percebi, u cant mix and match dynamic & static args either)
    //Por isso as funções de criação são as únicas "multiplicadas". Mas já é um improvement for scalability.

    public void CreateBounceVFX(Vector2 collisionPoint)
    {
        StartCoroutine(SetBounceVFX(collisionPoint));
    }

    public void CreateCoinVFX(Vector2 collisionPoint)
    {
        StartCoroutine(SetCoinVFX(collisionPoint));
    }

    private IEnumerator SetBounceVFX(Vector2 collisionPoint)
    {
        GameObject vfx = GetVFXFromPool(VFXType.Bounce);
        if (vfx == null)
        {
            yield break;
        }
        vfx.transform.position = new Vector3(collisionPoint.x, collisionPoint.y, 0);
        vfx?.SetActive(true);
        vfx.GetComponent<ParticleSystem>()?.Play();
        yield return new WaitForSeconds(0.6f);
        vfx?.SetActive(false);
    }

    private IEnumerator SetCoinVFX(Vector2 collisionPoint)
    {
        GameObject vfx = GetVFXFromPool(VFXType.Coin);
        if (vfx == null)
        {
            yield break;
        }
        vfx.transform.position = new Vector3(collisionPoint.x, collisionPoint.y, 0);
        vfx?.SetActive(true);
        vfx.GetComponent<ParticleSystem>()?.Play();
        yield return new WaitForSeconds(1f);
        vfx?.SetActive(false);
    }
}
