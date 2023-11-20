using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using FixedUpdate = UnityEngine.PlayerLoop.FixedUpdate;

public class moveCharacter : MonoBehaviour
{
    [SerializeField] float graviteAuSol = 1f; // Gravité lorsque le personnage est au sol
    [SerializeField] float graviteEnAir = 2f; // Gravité lorsque le personnage est en l'air
    private detectGround _detectGround;
    private CustomInput _input = null;
    private Vector2 _moveVector = Vector2.zero;
    private Vector2 _direction = Vector2.zero;
    [SerializeField] float deceleration = 0;
    [SerializeField] Rigidbody2D _rigidbody;

    [SerializeField] float _speed;
    [SerializeField] float _maxSpeed;


    private void Awake()
    {
        transform.position = Vector2.zero;
        _detectGround = GameObject.Find("raycast").GetComponent<detectGround>();
        _input = new CustomInput();
    }

    private void FixedUpdate()
    {
        if (_moveVector != Vector2.zero)
        {
            _direction += _moveVector;
        }

        _direction *= deceleration;
        _rigidbody.velocity = _speed * Time.deltaTime * _direction;
        //_rigidbody.gravityScale = _detectGround.isOnGround() ? graviteAuSol : graviteEnAir;

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
        if (_detectGround.isOnGround() && context.phase == InputActionPhase.Canceled)
        {
            _rigidbody.AddForce(Vector2.up * 1000000, ForceMode2D.Impulse);
        }
    }

}