using System.Collections;
using UnityEngine;

public class shootIA : MonoBehaviour
{
    private float _timer = 0;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _target;
    [SerializeField] private float _minRange;
    [SerializeField] private float _maxRange;
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
    private moveIA mIA;
    private float _wind;
    private void Awake()
    {
        _wind = GameObject.Find("shooter").GetComponent<shoot>().Wind();
        _transform = transform;
        _positionBullet = _transform.position;
        mIA = GameObject.Find("IA").GetComponent<moveIA>();
    }
    private void FixedUpdate()
    {
        _wind = GameObject.Find("shooter").GetComponent<shoot>().Wind();
        _acceleration.x = _wind;
        if (_timer < 3) 
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _previousPositionBullet = _transform.position;
            _speedV.x = Mathf.Cos(_angle * Mathf.Deg2Rad) * _force;
            _speedV.y = Mathf.Sin(_angle * Mathf.Deg2Rad) * _force;
            print(!mIA.is_move());
            if (!mIA.is_move())
            {
                for (int i = 0; i < 50; i++)
                {
                    if (_force >= 15 && _force2 >= 15 && _force3 >= 15 && _force4 >= 15)
                    {
                        _force = _force2 = _force3 = _force4 = 0;
                        _notFound = true;
                    }
                    if (_shot)
                    {
                        StopAllCoroutines();
                        _angle = 270;
                        _angle2 = 270 - 45;
                        _angle3 = 270 - 45 * 2;
                        _angle4 = 270 - 45 * 3;
                        _force = 1;
                        _force2 = 1;
                        _force3 = 1;
                        _force4 = 1;
                        break;
                    }
                    StartCoroutine(Search_1());
                    StartCoroutine(Search_2());
                    StartCoroutine(Search_3());
                    StartCoroutine(Search_4());
                }
            }

        }
    }
    /*private bool Loop()
    {

        _angle -= 0.05f;
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
            Debug.DrawLine(_previousPositionBullet, _positionBullet, Color.blue);
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

                _angle = 270;
                _force = 0;
                _timer = 0;
                return true;
            }
        }

        return false;
    }*/

    IEnumerator Search_4()
    {
        _angle4 -= 0.1f;
        if (_angle4 <= 270-45*4)
        {
            _force4++;
            _angle4 = 270-45*3;
        }

        if (_force4 >= 15)
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

            RaycastHit2D[] _raycast = Physics2D.CircleCastAll(_positionBullet, 0.4f,
                new Vector2(Mathf.Cos(_angle4 * Mathf.Deg2Rad), Mathf.Sin(_angle4 * Mathf.Deg2Rad)));

            if (_positionBullet.y < -5)
            {
                break;
            }
            foreach (var raycast in _raycast)
            {
                if (raycast.collider.GetComponent<BoxCollider2D>() != null && raycast.collider.GetComponent<CapsuleCollider2D>() == null)
                {
                    yield break;
                }
            }
            if (_target.GetComponent<CapsuleCollider2D>().OverlapPoint(_positionBullet) && !_shot)
            {
                var _ball = Instantiate(_projectile, _transform.position, Quaternion.identity);
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos((_angle4 + Random.Range(_minRange, _maxRange)) * Mathf.Deg2Rad) * (_force4 + Random.Range(_minRange, _maxRange)) * 1.01f, Mathf.Sin((_angle4 + Random.Range(_minRange, _maxRange)) * Mathf.Deg2Rad) * (_force4 + Random.Range(_minRange, _maxRange)) * 1.01f) ;
                _shot = true;

                _angle4 = 180;
                _force4 = 0;
                _timer = 0;
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
            _force3++;
            _angle3 = 270-45*2;
        }

        if (_force3 >= 15)
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

            RaycastHit2D[] _raycast = Physics2D.CircleCastAll(_positionBullet, 0.4f,
                new Vector2(Mathf.Cos(_angle3 * Mathf.Deg2Rad), Mathf.Sin(_angle3 * Mathf.Deg2Rad)));

            if (_positionBullet.y < -5)
            {
                break;
            }
            foreach (var raycast in _raycast)
            {
                if (raycast.collider.GetComponent<BoxCollider2D>() != null && raycast.collider.GetComponent<CapsuleCollider2D>() == null)
                {
                    yield break;
                }
            }
            if (_target.GetComponent<CapsuleCollider2D>().OverlapPoint(_positionBullet) && !_shot)
            {
                var _ball = Instantiate(_projectile, _transform.position, Quaternion.identity);
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos((_angle3 + Random.Range(_minRange, _maxRange)) * Mathf.Deg2Rad) * (_force3 + Random.Range(_minRange, _maxRange)) * 1.01f, Mathf.Sin((_angle3 + Random.Range(_minRange, _maxRange)) * Mathf.Deg2Rad) * (_force3 + Random.Range(_minRange, _maxRange)) * 1.01f);
                _shot = true;

                _angle3 = 180;
                _force3 = 0;
                _timer = 0;
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
            _force2++;
            _angle2 = 270-45;
        }

        if (_force2 >= 15)
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

            RaycastHit2D[] _raycast = Physics2D.CircleCastAll(_positionBullet, 0.4f,
                new Vector2(Mathf.Cos(_angle2 * Mathf.Deg2Rad), Mathf.Sin(_angle2 * Mathf.Deg2Rad)));

            if (_positionBullet.y < -5)
            {
                break;
            }
            foreach (var raycast in _raycast)
            {
                if (raycast.collider.GetComponent<BoxCollider2D>() != null && raycast.collider.GetComponent<CapsuleCollider2D>() == null)
                {
                    yield break;
                }
            }
            if (_target.GetComponent<CapsuleCollider2D>().OverlapPoint(_positionBullet) && !_shot)
            {
                var _ball = Instantiate(_projectile, _transform.position, Quaternion.identity);
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos((_angle2 + Random.Range(_minRange, _maxRange)) * Mathf.Deg2Rad) * (_force2 + Random.Range(_minRange, _maxRange)) * 1.01f, Mathf.Sin((_angle2 + Random.Range(_minRange, _maxRange)) * Mathf.Deg2Rad) * (_force2 + Random.Range(_minRange, _maxRange)) * 1.01f);
                _shot = true;

                _angle2 = 180;
                _force2 = 0;
                _timer = 0;
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
            _force++;
            _angle = 270;
        }

        if (_force >= 15)
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

            RaycastHit2D[] _raycast = Physics2D.CircleCastAll(_positionBullet, 0.4f,
                new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad), Mathf.Sin(_angle * Mathf.Deg2Rad)));

            if (_positionBullet.y < -5)
            {
                break;
            }
            foreach (var raycast in _raycast)
            {
                if (raycast.collider.GetComponent<BoxCollider2D>() != null && raycast.collider.GetComponent<CapsuleCollider2D>() == null)
                {
                    yield break;
                }
            }
            if (_target.GetComponent<CapsuleCollider2D>().OverlapPoint(_positionBullet) && !_shot)
            {
                var _ball = Instantiate(_projectile, _transform.position, Quaternion.identity);
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos((_angle + Random.Range(_minRange, _maxRange)) * Mathf.Deg2Rad) * (_force + Random.Range(_minRange, _maxRange)) * 1.01f, Mathf.Sin((_angle + Random.Range(_minRange, _maxRange)) * Mathf.Deg2Rad) * (_force + Random.Range(_minRange, _maxRange)) * 1.01f);
                _shot = true;

                _angle = 270;
                _force = 0;
                _timer = 0;
                break;
            }

        }
        yield return null;
    }

    /*private bool Search()
    {
        _force = (_force < 15) ? _force + .00095f : _force;
        _angle = (_angle >= 90) ? _angle -= 0.01f : _angle;
        if (_force >= 15 && _angle <= 90)
        {
            _notFound = true;
        }

        _positionBullet = _transform.position;
        var vSpawn = new Vector2(Mathf.Cos((_angle) * Mathf.Deg2Rad), Mathf.Sin((_angle) * Mathf.Deg2Rad)) * _force * 1.01f;
        _speedV = vSpawn;
        for (int i = 0; i < 400; i++)
        {
            Debug.DrawLine(_previousPositionBullet, _positionBullet, Color.blue);
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
                _timer = 0;
                return true;
            }
        }
        return false;
    }*/


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
