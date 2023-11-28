using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;
    private Vector3 _posOrigin = Vector3.zero;
    private Vector3 _tempPos = Vector3.zero;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _tempPos = Vector3.zero;
        _tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * _frequency) * _amplitude;
        _transform.localPosition = Vector3.up * 0.3f + _tempPos;
        GetComponent<SpriteRenderer>().flipX = (GetComponentInParent<Rigidbody2D>().velocity.x < -0.1f);
    }
}
