
using System;
using UnityEngine;
using UnityEngine.Events;

public class HoleHandling : MonoBehaviour
{

    public UnityEvent OnBallEnterHoleEvent;
    
    private static HoleHandling _instance;
    public static HoleHandling Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new HoleHandling();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Golf Ball"))
        {
            OnBallEnterHoleEvent.Invoke();
        }
    }
}
