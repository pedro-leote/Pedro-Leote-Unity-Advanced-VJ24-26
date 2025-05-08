using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallHandling : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    public UnityEvent OnBallSwingEvent;
    public UnityEvent<Vector2> OnBallBounceEvent;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 direction = new Vector2(1, 1);
            _rigidBody.AddForce(direction * 0.4f, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Vector2 direction = new Vector2(-1, 1);
            _rigidBody.AddForce(direction * 1.6f, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contactPoint = collision.GetContact(0);
        OnBallBounceEvent?.Invoke(contactPoint.point);
    }
}
