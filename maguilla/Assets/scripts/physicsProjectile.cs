using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class physicsProjectile : MonoBehaviour
{
    private float _wind;

    private Rigidbody2D _rb;
    private Vector2 _acceleration;
    private Vector2 _direction = Vector2.zero;
    private float _deceleration = 0.9f;

    private void Awake()
    {
        _wind = GameObject.Find("shooter").GetComponent<shoot>().Wind();
        _acceleration.x = _wind;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _wind = GameObject.Find("shooter").GetComponent<shoot>().Wind();
        _rb.velocity += _acceleration * Time.fixedDeltaTime;
    }
}
