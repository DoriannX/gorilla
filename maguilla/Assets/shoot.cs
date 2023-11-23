using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class shoot : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    private Rigidbody2D _rbProjectile;
    private bool _shooting = false;
    private bool _shootingController = false;
    private Transform _transform;

    private void Awake()
    {
        _rbProjectile = _projectile.GetComponent<Rigidbody2D>();
        _transform = transform;
    }

    private void Update()
    {
        if (_shooting)
        {
            Instantiate(_projectile, _transform.position, Quaternion.identity);
        }
    }

    public void ShootPerformed(InputAction.CallbackContext ctx)
    {
        _shooting = ctx.performed;
        _shootingController = false;
    }

    public void ShootPerformedController(InputAction.CallbackContext ctx)
    {
        _shooting = ctx.performed;
        _shootingController = true;
    }

    public bool getShooting()
    {
        return _shootingController;
    }
}
