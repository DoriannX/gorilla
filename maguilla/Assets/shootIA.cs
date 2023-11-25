using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class shootIA : MonoBehaviour
{
    private float _timer = 0;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _target;
    private Transform _transform;
    private Transform _transformTarget;

    private float _height;
    private Vector2 _halfRange;
    private float Vy;
    private Vector2 _distance;
    private Vector2 VXZ;
    [SerializeField] private float HauteurMax = 1f; // Hauteur maximale
    [SerializeField] float _masseBall = 1; // Masse de la balle (ajustez selon votre scène)
    [SerializeField] private float _g = 1; // Accélération due à la gravité
    [SerializeField] private GameObject _preVis;
    private float _angle;
    [Range(-10, 10)] public float _force = 1;


    private Vector2 _previousPositionBullet;
    private Vector2 _positionBullet;
    private Vector2 _acceleration;
    private Vector2 _speedV;
    private float _iteration;
    private float _speedSearch;
    private void Awake()
    {
        _transform = transform;
        _positionBullet = _transform.position;
        _iteration = 100;
        _angle = 270;
        _force = 3;
        _speedSearch = 0.05f;
    }
    private void FixedUpdate()
    {

        if (_timer < 0.1f)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _previousPositionBullet = _transform.position;
            _speedV = new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad), Mathf.Sin(_angle * Mathf.Deg2Rad)) * _force;
            _acceleration = new Vector2(0, -9.80665f);
            if (_iteration > 0)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (Loop())
                    {
                        break;
                    }
                }
            }
            else
            {
                _iteration = 100;
                _angle = 270;
                _force = 3;
                _speedSearch = 0.05f;
            }
        }
    }

    private bool Loop()
    {

        _angle -= _speedSearch;
        if(_angle <= 90)
        {
            _force++;
            _angle = 270;
        }

        _positionBullet = _transform.position;
        var vSpawn = new Vector2(Mathf.Cos((_angle) * Mathf.Deg2Rad), Mathf.Sin((_angle) * Mathf.Deg2Rad)) * _force * 1.01f;
        _speedV = vSpawn;
        for (int i = 0; i < 400; i++)
        {
            //Debug.DrawLine(_previousPositionBullet, _positionBullet, Color.blue);
            _previousPositionBullet = _positionBullet;
            _speedV += _acceleration * Time.fixedDeltaTime;
            _positionBullet += _speedV * Time.fixedDeltaTime;

            RaycastHit2D[] _raycast = Physics2D.CircleCastAll(_positionBullet, 0.5f,
                new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad), Mathf.Sin(_angle * Mathf.Deg2Rad)));

            if (_positionBullet.y < -5)
            {
                return false;
            }
            foreach (var raycast in _raycast)
            {
                if (raycast.collider.GetComponent<BoxCollider2D>() != null)
                {
                    return false;
                }
            }
            if (_target.GetComponent<CapsuleCollider2D>().OverlapPoint(_positionBullet))
            {
                var _ball = Instantiate(_projectile, _transform.position, Quaternion.identity);
                _ball.GetComponent<Rigidbody2D>().velocity = vSpawn;
                _iteration = 100;

                _angle = 270;
                _force = 1;
                return true;
            }
        }

        _iteration -= 1;
        return false;
    }

    
}
