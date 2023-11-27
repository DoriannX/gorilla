using UnityEngine;

public class moveIA : MonoBehaviour
{
    [SerializeField] private float _deceleration;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private shootIA _sIA;
    private detectWall _dw;
    private Vector3 _direction;
    private Rigidbody2D _rb;
    private float _timer = 0;
    private bool _move = false;

    private void Awake()
    {
        _direction = new Vector3(0, 0);
        _sIA = GetComponentInChildren<shootIA>();
        _rb = GetComponent<Rigidbody2D>();
        _dw = GetComponentInChildren<detectWall>();
    }
    private void Update()
    {
        if (_sIA.is_not_found()) _move = true;

        _direction *= _deceleration;

        _rb.velocity += new Vector2(_speed * _direction.x - _rb.velocity.x, 0);

        if (_move)
        {
            if (_timer < 1)
            {
                _direction += Vector3.left;
                _timer += Time.deltaTime;
            }
            else
            {
                _sIA.set_is_not_found(false);
                _move = false;
                _timer = 0;
            }
        }

        if (_dw.is_wall_in_front())
        {
            _timer = 0;
            _rb.velocity += Vector2.up * _jumpForce;
            _dw.set_wall_in_front(false);
        }
    }

    public bool is_move()
    {
        return _move;
    }
}
