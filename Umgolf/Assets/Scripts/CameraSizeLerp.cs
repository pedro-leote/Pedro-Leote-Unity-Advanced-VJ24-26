using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeLerp : MonoBehaviour
{
    private Camera _camera;
    private float _goalSize = 0;
    private bool _canBeginLerp = false;
    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }
    private void Update()
    {
        if (Mathf.Approximately(_camera.orthographicSize, _goalSize))
        {
            _canBeginLerp = false;
        }
        
        if (_canBeginLerp)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _goalSize, Time.deltaTime * 5f);
        }


    }
    public void StartLerp(float size)
    {
        _goalSize = size;
        _canBeginLerp = true;
    }
    
    public void StopLerp()
    {
        _canBeginLerp = false;
    }
}
