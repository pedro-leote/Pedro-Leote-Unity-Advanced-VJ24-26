using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeLerp : MonoBehaviour
{
    private Camera _camera;
    private float _goalSize = 0;
    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }
    
    public void CameraSizeLerpTo(float size)
    {
        StartCoroutine(LerpTo(size));
    }

    private IEnumerator LerpTo(float size)
    {
        _goalSize = size;
        while (_camera.orthographicSize != _goalSize || _goalSize != 0)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _goalSize, Time.deltaTime * 10f);
        }
        _goalSize = 0;
        yield return null;
    }
}
