using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisabler : MonoBehaviour
{
    [SerializeField] private float _timeToWait;

    public void DisableObject()
    {
        StartCoroutine(DisableAction(_timeToWait));
    }
    
    private IEnumerator DisableAction(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
