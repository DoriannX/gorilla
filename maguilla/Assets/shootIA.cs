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
    [SerializeField] private float _angle;
    [Range(-10, 10)] public float _speed;
    [Range(-10, 10)] public float _force = 1;


    private Vector2 _previousPositionBullet;
    private Vector2 _positionBullet;
    private Vector2 _acceleration;
    private Vector2 _speedV;
    private float _iteration;
    private void Awake()
    {
        _transform = transform;
        _positionBullet = _transform.position;
        _iteration = 100;
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
            _speedV = new Vector2(_speed, 1) * _force;
            _acceleration = new Vector2(0, -9.81f);
            _positionBullet = _transform.position;
            if (_iteration > 0)
            {
                _iteration -= 1;
                _speed -= 1f;

                for (int i = 0; i < 1000; i++)
                {
                    if (_target.GetComponent<CapsuleCollider2D>().OverlapPoint(_positionBullet))
                    {
                        var vSpawn = new Vector2(_speed, 1) * _force;

                        var _ball = Instantiate(_projectile, _transform.position, Quaternion.identity);
                        _ball.GetComponent<Rigidbody2D>().velocity = vSpawn;
                        _iteration = 1000;

                        _timer = 0;
                        _speed = 0;
                        break;
                    }

                    _previousPositionBullet = _positionBullet;
                    _speedV += _acceleration * Time.fixedDeltaTime;
                    _positionBullet += _speedV * Time.fixedDeltaTime;
                    Debug.DrawLine(_previousPositionBullet, _positionBullet, Color.blue);
                }

            }
        }
    }

    public void LaunchBall(GameObject ball, float angle)
    {
        _transform = ball.transform;
        _transformTarget = _target.transform;
        var _targetPos = _transformTarget.position;
        // Convertir l'angle en radians
        float rad = angle * Mathf.Deg2Rad;

        // Calculer la distance horizontale entre la balle et la target
        float dx = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(_targetPos.x, 0));

        // Calculer la hauteur initiale de la balle
        float y0 = transform.position.y;

        // Calculer la hauteur de la target
        float yf = _targetPos.y;

        // Calculer la vitesse initiale de la balle en m/s
        // En utilisant l'équation de la trajectoire parabolique
        // y = y0 + x * tan(rad) - (g * x^2) / (2 * v0^2 * cos(rad)^2)
        // On obtient une équation du second degré en v0
        // a * v0^2 + b * v0 + c = 0
        // Où a = - (g * dx^2) / (2 * cos(rad)^2)
        // b = 0
        // c = y0 - yf - dx * tan(rad)
        // On cherche la plus petite racine positive de cette équation
        float a = -(-1 * dx * dx) / (2 * Mathf.Pow(Mathf.Cos(rad), 2));
        float b = 0;
        float c = y0 - yf - dx * Mathf.Tan(rad);
        float delta = b * b - 4 * a * c;
        if (delta < 0)
        {
            // Il n'y a pas de solution réelle
            Debug.LogError("Impossible d'atteindre la target avec cette direction");
            return;
        }
        else
        {
            // Il y a deux solutions réelles
            float v01 = (-b - Mathf.Sqrt(delta)) / (2 * a);
            float v02 = (-b + Mathf.Sqrt(delta)) / (2 * a);
            // On choisit la plus petite racine positive
            float v0 = Mathf.Min(v01, v02);
            if (v0 <= 0)
            {
                // Il n'y a pas de solution positive
                Debug.LogError("Impossible d'atteindre la target avec cette direction");
                return;
            }
            else
            {
                // On a trouvé la vitesse initiale
                // Calculer la force à appliquer à la balle en N
                var force = 1 * v0 * new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) / Time.fixedDeltaTime;

                // Appliquer la force à la balle
                ball.GetComponent<Rigidbody2D>().velocity = force;
            }
        }
    }
}
