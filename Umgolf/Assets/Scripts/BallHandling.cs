using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BallHandling : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    
    private bool _canStartInput;
    [SerializeField] private InputActionReference _holdAction;

    private Vector2 _fingerPosition;
    private float _powerAccumulated = 0f;
    [SerializeField] private float _maxPowerPossible = 6.5f;
    
    
    public UnityEvent OnBallSwingEvent;
    public UnityEvent<Vector2> OnBallBounceEvent;

    private void OnEnable()
    {
        _holdAction.action.Enable();
    }
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        _holdAction.action.started += HoldStarted;
        _holdAction.action.performed += HoldContinue;
        _holdAction.action.canceled += HoldRelease;
    }

    void Start()
    {
        
    }
    void Update()
    {

    }
    //TODO: Implement Touch Controls:
    //-> Start pressing -> Gizmo control, set max radius, vector from ball to location.
    //-> Release Press -> Ball.AddForce based on Vector of trajectory and multiplied by the strength of the swing (the radius)
    //Prevent subsequent happenings

    private void HoldStarted(InputAction.CallbackContext obj)
    {
        Debug.Log($"Started Touch Handling at: {obj.ReadValue<Vector2>()}");
    }
    private void HoldContinue(InputAction.CallbackContext obj)
    {
        while (_holdAction.action.inProgress)
        {
            Debug.Log($"Continued at: {obj.ReadValue<Vector2>()}");
        }
    }
    private void HoldRelease(InputAction.CallbackContext obj)
    {
        Debug.Log($"Released at: {obj.ReadValue<Vector2>()}");
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contactPoint = collision.GetContact(0);
        OnBallBounceEvent?.Invoke(contactPoint.point);
    }

    private void OnDisable()
    {
        _holdAction.action.Disable();
    }
}
