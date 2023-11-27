using System;
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
    private void Awake()
    {
        _rbProjectile = _projectile.GetComponent<Rigidbody2D>();
        _transform = transform;
        _mc = _player.GetComponent<moveCharacter>();
        _direction = Vector2.one;
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
        
        _direction = _mc.getMousePos() - (Vector2)_transform.position;
        _direction.x = Mathf.Clamp(_direction.x, 0, Single.PositiveInfinity);
        _direction.y = Mathf.Clamp(_direction.y, 0, Single.PositiveInfinity);
        var vSpawn = _direction;
;
        _speedV = vSpawn * _force;

        _positionBullet = _transform.position;
        _previousPositionBullet = _positionBullet;
        for (int i = 0; i < 400; i++)
        {
            _previousPositionBullet = _positionBullet;
            _speedV += _acceleration * Time.fixedDeltaTime;
            _positionBullet += _speedV * Time.fixedDeltaTime;
            _trajectoryDots[i/4].SetActive(true);
            _trajectoryDots[i/4].transform.position = _positionBullet;

            RaycastHit2D[] _raycast = Physics2D.CircleCastAll(_positionBullet, 0.5f,
                _mc.getMousePos() - (Vector2)_transform.position);

            foreach (var raycast in _raycast)
            {
                if (raycast.collider.GetComponent<BoxCollider2D>() != null)
                {
                    for(int j = i/4; j < _trajectoryDots.Length; j++)
                    {
                        _trajectoryDots[j].SetActive(false);
                    }
                    return false;
                }
            }
        }
        
        return false;
    }

    public void ShootPerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _timer >= 3)
        {
            var projectile = Instantiate(_projectile, _transform.position, Quaternion.identity);
            _direction = _mc.getMousePos() - (Vector2)_transform.position;
            _direction.x = Mathf.Clamp(_direction.x, 0, Single.PositiveInfinity);
            _direction.y = Mathf.Clamp(_direction.y, 0, Single.PositiveInfinity);
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
