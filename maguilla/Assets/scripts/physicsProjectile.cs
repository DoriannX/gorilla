using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class physicsProjectile : MonoBehaviour
{
    private float _wind;

    private Rigidbody2D _rb;
    private Vector2 _acceleration;
    private Vector2 _direction = Vector2.zero;
    [SerializeField] private ParticleSystem ps;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Instantiate(ps, transform.position + (transform.position - (Vector3)collision.contacts[0].point).normalized * 0.1f, Quaternion.Euler(transform.position - (Vector3)collision.contacts[0].point));
    }
}
