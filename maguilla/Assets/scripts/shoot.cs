using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class shoot : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _force;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _trajectoryDot;
    [SerializeField] private GameObject _trajectory;
    [SerializeField] private float _wind;

    private Rigidbody2D _rbProjectile;
    private bool _shootingController = false;
    private Transform _transform;
    private moveCharacter _mc;
    private Vector2 _direction;
    private float _timer;
    private Vector2 _positionBullet;
    private Vector2 _previousPositionBullet;
    private Vector2 _speedV;
    private Vector2 _acceleration;
    private GameObject[] _trajectoryDots = new GameObject[100];
    private Mouse mouse;
    private void Awake()
    {
        _rbProjectile = _projectile.GetComponent<Rigidbody2D>();
        _transform = transform;
        _mc = _player.GetComponent<moveCharacter>();
        _direction = Vector2.zero;
        _timer = 0;
        _positionBullet = _transform.position;
        _previousPositionBullet = _positionBullet;
        _acceleration = new Vector2(_wind, -9.80665f);
        _wind = Random.Range(-15, 15);
        print(_wind);
        for (int i = 0; i < 100; i++)
        {
            _trajectoryDots[i] = Instantiate(_trajectoryDot, _positionBullet, Quaternion.identity, _trajectory.transform);
            _trajectoryDots[i].SetActive(true);
        }
        mouse = Mouse.current;

    }

    private void Update()
    {
        _timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _acceleration.x = _wind;
        Loop();
    }

    private bool Loop()
    {
        
        _direction += mouse.delta.ReadValue() * 0.1f;
        var vSpawn = _direction;
;
        _speedV = vSpawn * _force;

        _positionBullet = _transform.position;
        _previousPositionBullet = _positionBullet;
        for (int i = 0; i < 50; i++)
        {
            _previousPositionBullet = _positionBullet;
            _speedV += _acceleration * Time.fixedDeltaTime;
            _positionBullet += _speedV * Time.fixedDeltaTime;
            _trajectoryDots[i/4].SetActive(true);
            _trajectoryDots[i/4].transform.position = _positionBullet;

            RaycastHit2D[] _raycast = Physics2D.CircleCastAll(_positionBullet, 0.1f, Vector2.zero);
            
            foreach (var raycast in _raycast)
            {
                if (raycast.collider.gameObject.tag == "wall")
                {

                    DrawCircleRaycast(_positionBullet);
                    for (int j = i/4; j < _trajectoryDots.Length; j++)
                    {
                        _trajectoryDots[j].SetActive(false);
                    }
                    return false;
                }
            }
        }
        
        return false;
    }

    public float radius = 0.1f; // Le rayon du cercle
    public int numRays = 360; // Le nombre de rayons à dessiner

    // Dessine un cercle de rayons et renvoie tous les objets touchés
    public void DrawCircleRaycast(Vector2 center)
    {

        for (int i = 0; i < numRays; i++)
        {
            float angle = (float)i / numRays * 360f; // Calcule l'angle pour ce rayon
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right; // Calcule la direction du rayon

                // Dessine une ligne du centre du cercle dans la direction du rayon
            Debug.DrawRay(center, direction * radius, Color.green);
        }

    }

    public void ShootPerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _timer >= 3)
        {
            var projectile = Instantiate(_projectile, _transform.position, Quaternion.identity);
            _direction += mouse.delta.ReadValue();
            projectile.GetComponent<Rigidbody2D>().velocity += _direction * _force;
            _timer = 0;

        }
    }

    public void ShootPerformedController(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            var projectile = Instantiate(_projectile, _transform.position, Quaternion.identity);
            _direction = new Vector2(1, 1);
            if (_mc.getDirection() != Vector2.zero)
            {
                _direction = _mc.getDirection();
                _direction.x = Mathf.Clamp(_direction.x, 0, 1);
                _direction.y = Mathf.Clamp(_direction.y, 0, 1);
            }

            if (_direction == Vector2.zero)
            {
                _direction = new Vector2(1, 1);
            }
            projectile.GetComponent<Rigidbody2D>().velocity += _direction * _force;
        }
    }

    public bool getShooting()
    {
        return _shootingController;
    }

    public float Wind()
    {
        return _wind;
    }
}
