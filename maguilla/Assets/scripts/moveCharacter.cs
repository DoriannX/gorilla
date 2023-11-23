using UnityEngine;
using UnityEngine.InputSystem;

public class moveCharacter : MonoBehaviour
{
    private detectGround _detectGround;
    private CustomInput _input = null;
    private Vector2 _moveVector = Vector2.zero;
    private Vector2 _direction = Vector2.zero;
    [SerializeField] float deceleration = 0;
    [SerializeField] Rigidbody2D _rigidbody;

    [SerializeField] float _speed;
    [SerializeField] float _maxSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] addforce _af;
    private Vector2 _projectileDirection;
    private Vector2 _mousePos;
    private Camera _camera;

    private void Awake()
    {
        transform.position = new Vector3(-8.625f, 0);
        _detectGround = GameObject.Find("raycast").GetComponent<detectGround>();
        _input = new CustomInput();
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {

        if (_moveVector != Vector2.zero)
        {
            _direction += _moveVector;
        }

        _direction *= deceleration;
        _rigidbody.velocity += new Vector2(_speed * Time.deltaTime * _direction.x - _rigidbody.velocity.x, 0);
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.player.Movement.performed += onMovementPerformed;
        _input.player.Movement.canceled += onMovementCanceled;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.player.Movement.performed -= onMovementPerformed;
        _input.player.Movement.canceled -= onMovementCanceled;
    }

    public void onMovementPerformed(InputAction.CallbackContext context)
    {
        _moveVector = context.ReadValue<Vector2>();
    }

    private void onMovementCanceled(InputAction.CallbackContext context)
    {
        _moveVector = Vector2.zero;
    }

    public void jump(InputAction.CallbackContext context)
    {
        if (_detectGround.isOnGround() && context.phase == InputActionPhase.Performed)
        {
            _rigidbody.velocity += Vector2.up*_jumpForce;
        }
    }

    public void gatherDirection(InputAction.CallbackContext context)
    {
        _projectileDirection = context.ReadValue<Vector2>();
    }

    public Vector2 getDirection()
    {
        return _projectileDirection;
    }



    public void mousePos(InputAction.CallbackContext ctx)
    {
        _mousePos = ctx.ReadValue<Vector2>();
        _mousePos = _camera.ScreenToWorldPoint(_mousePos);
    }

    public Vector2 getMousePos()
    {
        return _mousePos;
    }
}