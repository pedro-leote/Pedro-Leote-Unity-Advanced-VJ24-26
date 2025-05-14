using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BallHandling : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private LineRenderer _referenceLineRenderer;
    
    [SerializeField] private InputActionReference _holdAction;
    [SerializeField] private InputActionReference _holdButtonAction;
    
    private float _powerAccumulated = 0f;
    [SerializeField] private float _maxPowerPossible = 30f;
    
    public UnityEvent OnBallEnabledEvent;
    public UnityEvent OnBallSwingEvent;
    public UnityEvent<Vector2> OnBallBounceEvent;

    private Vector2 _startingBallPosition = new Vector2(0, -3);
    private Vector2 _initialTouchPosition;
    [SerializeField] Vector2 _currentTouchPosition;
    [SerializeField] Vector2 _distanceFromBall;
    
    private static BallHandling _instance;
    public static BallHandling Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BallHandling();
            }
            return _instance;
        }
    }
    
    private void OnEnable()
    {
        _holdAction.action.Enable();
        _holdButtonAction.action.Enable();
        
        OnBallEnabledEvent?.Invoke();
        
        _referenceLineRenderer.positionCount = 2;
        transform.position = _startingBallPosition;
        _rigidBody.velocity = Vector2.zero;
    }
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _referenceLineRenderer = GetComponent<LineRenderer>();

        _holdAction.action.started += HoldStarted;
        _holdAction.action.performed += HoldContinue;
        _holdAction.action.canceled += HoldRelease;
        
        _holdButtonAction.action.started += HoldButtonStarted;
        _holdButtonAction.action.canceled += HoldButtonReleased;
    }

    void Start()
    {
        _distanceFromBall = transform.position;
    }
    private void HoldButtonStarted(InputAction.CallbackContext obj)
    {
        this.enabled = true;
        _initialTouchPosition = _currentTouchPosition;
    }

    private void HoldButtonReleased(InputAction.CallbackContext obj)
    {
        
        this.enabled = false;
    }
    private void HoldStarted(InputAction.CallbackContext obj)
    {
        _currentTouchPosition = Camera.main.ScreenToWorldPoint(obj.ReadValue<Vector2>());
    }
    private void HoldContinue(InputAction.CallbackContext obj)
    {
        _currentTouchPosition = Camera.main.ScreenToWorldPoint(obj.ReadValue<Vector2>());
        Vector2 direction = _distanceFromBall - _currentTouchPosition;
        _powerAccumulated = direction.magnitude * 9f;
        
        Camera.main.orthographicSize = Mathf.Clamp(5f + (direction.magnitude * 0.33f), 5f, 6.3f);
        
        _referenceLineRenderer.SetPosition(0, _startingBallPosition);
        _referenceLineRenderer.SetPosition(1, _startingBallPosition + Vector2.ClampMagnitude((direction * _powerAccumulated) / 25f, _maxPowerPossible / 25f));
        
    }
    private void HoldRelease(InputAction.CallbackContext obj)
    {
        //_currentTouchPosition = Camera.main.ScreenToWorldPoint(obj.ReadValue<Vector2>());
        float distance = Vector2.Distance(_distanceFromBall, _currentTouchPosition);
        if (distance < 0.8)
        {
            return;
        }
        Vector2 direction = _distanceFromBall - _currentTouchPosition;
        
        //_rigidBody.velocity = Vector2.ClampMagnitude(direction * _powerAccumulated, _maxPowerPossible);
        _rigidBody.AddForce(Vector2.ClampMagnitude(direction * _powerAccumulated, _maxPowerPossible));
    }

    public void BallReachingHoleWrapper()
    {
        StartCoroutine(OnBallReachingHole());
    }

    private IEnumerator OnBallReachingHole()
    {
        _rigidBody.velocity = Vector2.zero;
        
        //TODO: All this grabbing of the Hole is long & impractical... perhaps init a variable with it on Start()?
		float distance = Vector3.Distance(HoleHandling.Instance.gameObject.transform.position, transform.position);

		
		while(distance > 0.03f)
		{
            
        	transform.position = Vector3.Lerp(transform.position, HoleHandling.Instance.gameObject.transform.position, 0.2f);
            distance = Vector3.Distance(HoleHandling.Instance.gameObject.transform.position, transform.position);
            
        	yield return null;
		}
		yield return null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contactPoint = collision.GetContact(0);
        OnBallBounceEvent?.Invoke(contactPoint.point);
    }

    private void OnDisable()
    {
        _referenceLineRenderer.positionCount = 0;
        OnBallSwingEvent?.Invoke();
        _holdAction.action.Disable();
        //_holdButtonAction.action.Disable();
    }
}
