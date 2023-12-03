using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class shootIA : MonoBehaviour
{
    private float _timer = 0;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _target;
    [SerializeField] private float _minRange;
    [SerializeField] private float _maxRange;
    [SerializeField] private float _maxForce;
    [SerializeField] private float _stepForce;
    [SerializeField] private GameObject _shooter, _ia, _player;
    private Transform _transform;

    private float _angle = 270;
    private float _angle2 = 270 - 45;
    private float _angle3 = 270 - 45 * 2;
    private float _angle4 = 270-45 * 3;
    private float _force, _force2, _force3, _force4 = 0;


    private Vector2 _previousPositionBullet;
    private Vector2 _positionBullet;
    private Vector2 _acceleration = new Vector2(0, -9.80665f);
    private Vector2 _speedV;
    private bool _notFound = false;
    private Coroutine _coroutine;
    private Vector2 vSpawn = Vector2.zero;
    private bool _shot = false;
    [SerializeField] private moveIA _mIA;
    private float _wind = 0;
    private float _shootCadence;
    private CapsuleCollider2D _targetCapsuleCollider2D;
    private moveCharacter _moveCharacter;
    private void Awake()
    {
        _transform = transform;
        _positionBullet = _transform.position;
        _targetCapsuleCollider2D = _target.GetComponent<CapsuleCollider2D>();
        _moveCharacter = _player.GetComponent<moveCharacter>();
    }

    private void Update()
    {
        switch (levelManager._difficulty)
        {
            case 0: _shootCadence = 3;
                _minRange = _maxRange = 2; break;
            case 1: _shootCadence = 1; _minRange = _maxRange = 1; break;
            case 2: _shootCadence = 0.5f; _minRange = _maxRange = 0.5f; break;
        }
    }
    private void FixedUpdate()
    {
        if (_shooter != null) if (_shooter.TryGetComponent<shoot>(out shoot temp)) _wind = temp.Wind();
        _acceleration.x = _wind;
        if (_timer < _shootCadence) 
        {
            _timer += Time.fixedDeltaTime;
        }
        else
        {
            _previousPositionBullet = _transform.position;
            _speedV.x = Mathf.Cos(_angle * Mathf.Deg2Rad) * _force;
            _speedV.y = Mathf.Sin(_angle * Mathf.Deg2Rad) * _force;

            Coroutine(180);
            if (!_mIA.is_move())
            {
                /*for (int i = 0; i < 50; i++)
                {
                    if (_force >= _maxForce && _force2 >= _maxForce && _force3 >= _maxForce && _force4 >= _maxForce)
                    {
                        _force = _force2 = _force3 = _force4 = 0;
                        
                    }
                    if (_shot)
                    {
                        StopAllCoroutines();
                        _angle = 270;
                        _angle2 = 270 - 45;
                        _angle3 = 270 - 45 * 2;
                        _angle4 = 270 - 45 * 3;
                        _force = 0;
                        _force2 = 0;
                        _force3 = 0;
                        _force4 = 0;
                        break;
                    }

                    
                    /*StartCoroutine(Search_1());
                    StartCoroutine(Search_2());
                    StartCoroutine(Search_3());
                    StartCoroutine(Search_4());#1#
                }*/
            }

        }
    }
    private void Coroutine(int num)
    {
        float base_angle = 180 / num;
        _force += _stepForce;

        if (_force >= _maxForce)
        {
            _notFound = true;
            _force = 0;
            return;
        }

        for (int i = 0; i < num; i++)
        {
            float _angleOffSet = base_angle * i + 180;
            StartCoroutine(Search(_angle + _angleOffSet));
        }
    }

    IEnumerator Search(float angle)
    {
        _positionBullet = _transform.position;
        vSpawn.x = Mathf.Cos((angle) * Mathf.Deg2Rad) * _force * 1.01f;
        vSpawn.y = Mathf.Sin((angle) * Mathf.Deg2Rad) * _force * 1.01f;
        _speedV = vSpawn;
        for (int i = 0; i < 400; i++)
        {
            
            Debug.DrawLine(_previousPositionBullet, _positionBullet, Color.blue);
            _previousPositionBullet = _positionBullet;
            _speedV += _acceleration * Time.fixedDeltaTime;
            _positionBullet += _speedV * Time.fixedDeltaTime;

            RaycastHit2D[] _raycast = Physics2D.CircleCastAll(_positionBullet, 0.5f,
                Vector2.zero);

            if (_positionBullet.y < -15)
            {
                break;
            }
            foreach (var raycast in _raycast)
            {
                if (raycast.collider.tag == "wall")
                {
                    yield break;
                }
            }
            if (_targetCapsuleCollider2D.OverlapPoint(_positionBullet) && !_shot)
            {
                var _ball = Instantiate(_projectile, _transform.position, Quaternion.identity);
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos((angle + Random.Range(_minRange, _maxRange)) * Mathf.Deg2Rad) * (_force + Random.Range(_minRange, _maxRange)) * 1.01f, Mathf.Sin((angle + Random.Range(_minRange, _maxRange)) * Mathf.Deg2Rad) * (_force + Random.Range(_minRange, _maxRange)) * 1.01f);
                _shot = true;
                _timer = 0;
                if (Random.Range(0, 100) < 50)
                {
                    _mIA.Move(false);
                }
                else
                {
                    _mIA.Move(true);
                }
                break;
            }

        }

        yield return new WaitForSeconds(1);
        _shot = false;
        yield return null;
    }

    IEnumerator Search_4()
    {
        _angle4 -= 0.1f;
        if (_angle4 <= 270-45*4)
        {
            _force4 += _stepForce;
            _angle4 = 270-45*3;
        }

        if (_force4 >= _maxForce)
        {
            yield break;
        }

        _positionBullet = _transform.position;
        vSpawn.x = Mathf.Cos((_angle4) * Mathf.Deg2Rad) * _force4 * 1.01f;
        vSpawn.y = Mathf.Sin((_angle4) * Mathf.Deg2Rad) * _force4 * 1.01f;
        _speedV = vSpawn;
        for (int i = 0; i < 400; i++)
        {
            Debug.DrawLine(_previousPositionBullet, _positionBullet, Color.blue);
            _previousPositionBullet = _positionBullet;
            _speedV += _acceleration * Time.fixedDeltaTime;
            _positionBullet += _speedV * Time.fixedDeltaTime;

            RaycastHit2D[] _raycast = Physics2D.CircleCastAll(_positionBullet, 0.5f,
                Vector2.zero);

            if (_positionBullet.y < -15)
            {
                break;
            }
            foreach (var raycast in _raycast)
            {
                if (raycast.collider.tag == "wall")
                {
                    yield break;
                }
            }
            if (_targetCapsuleCollider2D.OverlapPoint(_positionBullet) && !_shot)
            {
                var _ball = Instantiate(_projectile, _transform.position, Quaternion.identity);
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos((_angle4 + Random.Range(_minRange, _maxRange)) * Mathf.Deg2Rad) * (_force4 + Random.Range(_minRange, _maxRange)) * 1.01f, Mathf.Sin((_angle4 + Random.Range(_minRange, _maxRange)) * Mathf.Deg2Rad) * (_force4 + Random.Range(_minRange, _maxRange)) * 1.01f) ;
                _shot = true;

                _angle4 = 180;
                _force4 = 0;
                _timer = 0;
                if (Random.Range(0, 100) < 50)
                {
                    _mIA.Move(false);
                }
                else
                {
                    _mIA.Move(true);
                }
                break;
            }

        }
        yield return null;
    }
    IEnumerator Search_3()
    {
        _angle3 -= 0.1f;
        if (_angle3 <= 270-45*3)
        {
            _force3 += _stepForce;
            _angle3 = 270-45*2;
        }

        if (_force3 >= _maxForce)
        {
            yield break;
        }

        _positionBullet = _transform.position;
        vSpawn.x = Mathf.Cos((_angle3) * Mathf.Deg2Rad) * _force3 * 1.01f;
        vSpawn.y = Mathf.Sin((_angle3) * Mathf.Deg2Rad) * _force3 * 1.01f;
        _speedV = vSpawn;
        for (int i = 0; i < 400; i++)
        {
            Debug.DrawLine(_previousPositionBullet, _positionBullet, Color.blue);
            _previousPositionBullet = _positionBullet;
            _speedV += _acceleration * Time.fixedDeltaTime;
            _positionBullet += _speedV * Time.fixedDeltaTime;

            RaycastHit2D[] _raycast = Physics2D.CircleCastAll(_positionBullet, 0.5f,
                Vector2.zero);

            if (_positionBullet.y < -15)
            {
                break;
            }
            foreach (var raycast in _raycast)
            {
                if (raycast.collider.tag == "wall")
                {
                    yield break;
                }
            }
            if (_targetCapsuleCollider2D.OverlapPoint(_positionBullet) && !_shot)
            {
                var _ball = Instantiate(_projectile, _transform.position, Quaternion.identity);
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos((_angle3 + Random.Range(_minRange, _maxRange)) * Mathf.Deg2Rad) * (_force3 + Random.Range(_minRange, _maxRange)) * 1.01f, Mathf.Sin((_angle3 + Random.Range(_minRange, _maxRange)) * Mathf.Deg2Rad) * (_force3 + Random.Range(_minRange, _maxRange)) * 1.01f);
                _shot = true;

                _angle3 = 180;
                _force3 = 0;
                _timer = 0;
                if (Random.Range(0, 100) < 50)
                {
                    _mIA.Move(false);
                }
                else
                {
                    _mIA.Move(true);
                }
                break;

            }

        }
        yield return null;
    }
    IEnumerator Search_2()
    {
        _angle2 -= 0.1f;
        if (_angle2 <= 270-45*2)
        {
            _force2 += _stepForce;
            _angle2 = 270-45;
        }

        if (_force2 >= _maxForce)
        {
            yield break;
        }

        _positionBullet = _transform.position;
        vSpawn.x = Mathf.Cos((_angle2) * Mathf.Deg2Rad) * _force2 * 1.01f;
        vSpawn.y = Mathf.Sin((_angle2) * Mathf.Deg2Rad) * _force2 * 1.01f;
        _speedV = vSpawn;
        for (int i = 0; i < 400; i++)
        {
            Debug.DrawLine(_previousPositionBullet, _positionBullet, Color.blue);
            _previousPositionBullet = _positionBullet;
            _speedV += _acceleration * Time.fixedDeltaTime;
            _positionBullet += _speedV * Time.fixedDeltaTime;

            RaycastHit2D[] _raycast = Physics2D.CircleCastAll(_positionBullet, 0.5f,
                Vector2.zero);

            if (_positionBullet.y < -15)
            {
                break;
            }
            foreach (var raycast in _raycast)
            {
                if (raycast.collider.tag == "wall")
                {
                    yield break;
                }
            }
            if (_targetCapsuleCollider2D.OverlapPoint(_positionBullet) && !_shot)
            {
                var _ball = Instantiate(_projectile, _transform.position, Quaternion.identity);
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos((_angle2 + Random.Range(_minRange, _maxRange)) * Mathf.Deg2Rad) * (_force2 + Random.Range(_minRange, _maxRange)) * 1.01f, Mathf.Sin((_angle2 + Random.Range(_minRange, _maxRange)) * Mathf.Deg2Rad) * (_force2 + Random.Range(_minRange, _maxRange)) * 1.01f);
                _shot = true;

                _angle2 = 180;
                _force2 = 0;
                _timer = 0;
                if (Random.Range(0, 100) < 50)
                {
                    _mIA.Move(false);
                }
                else
                {
                    _mIA.Move(true);
                }
                break;
            }

        }
        yield return null;
    }

    IEnumerator Search_1()
    {
        _angle -= 0.1f;
        if (_angle <= 270-45)
        {
            _force += _stepForce;
            _angle = 270;
        }

        if (_force >= _maxForce)
        {
            yield break;
        }

        _positionBullet = _transform.position;
        vSpawn.x = Mathf.Cos((_angle) * Mathf.Deg2Rad) * _force * 1.01f;
        vSpawn.y = Mathf.Sin((_angle) * Mathf.Deg2Rad) * _force * 1.01f;
        _speedV = vSpawn;
        for (int i = 0; i < 400; i++)
        {
            Debug.DrawLine(_previousPositionBullet, _positionBullet, Color.blue);
            _previousPositionBullet = _positionBullet;
            _speedV += _acceleration * Time.fixedDeltaTime;
            _positionBullet += _speedV * Time.fixedDeltaTime;

            RaycastHit2D[] _raycast = Physics2D.CircleCastAll(_positionBullet, 0.5f,
                Vector2.zero);

            if (_positionBullet.y < -15)
            {
                break;
            }
            foreach (var raycast in _raycast)
            {
                if (raycast.collider.tag == "wall")
                {
                    yield break;
                }
            }
            if (_targetCapsuleCollider2D.OverlapPoint(_positionBullet) && !_shot)
            {
                var _ball = Instantiate(_projectile, _transform.position, Quaternion.identity);
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos((_angle + Random.Range(_minRange, _maxRange)) * Mathf.Deg2Rad) * (_force + Random.Range(_minRange, _maxRange)) * 1.01f, Mathf.Sin((_angle + Random.Range(_minRange, _maxRange)) * Mathf.Deg2Rad) * (_force + Random.Range(_minRange, _maxRange)) * 1.01f);
                _shot = true;

                _angle = 270;
                _force = 0;
                _timer = 0;
                if (Random.Range(0, 100) < 50)
                {
                    _mIA.Move(false);
                }
                else
                {
                    _mIA.Move(true);
                }
                break;
            }

        }
        yield return null;
    }

    public bool is_not_found()
    {
        return _notFound;
    }

    public void set_is_not_found(bool state)
    {
        _notFound = state;

        _angle = 270;
        _force = 1;
    }

    public void reset_shot()
    {
        _shot = false;
    }
}
