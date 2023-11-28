using UnityEngine;

public class physicsProjectile : MonoBehaviour
{
    private float _wind;

    private Rigidbody2D _rb;
    private Vector2 _acceleration;
    private Vector2 _direction = Vector2.zero;

    private void Awake()
    {
        GameObject.Find("shooter").TryGetComponent<shoot>(out shoot temp);
        _wind = temp.Wind();
        _acceleration.x = _wind;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        GameObject.Find("shooter").TryGetComponent<shoot>(out shoot temp);
        _wind = temp.Wind();
        _rb.velocity += _acceleration * Time.fixedDeltaTime;
    }
}
