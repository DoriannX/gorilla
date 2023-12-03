using UnityEngine;

public class fireParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fire;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _fire = Instantiate(_fire, transform);
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _fire.transform.rotation = Quaternion.Euler(new Vector2(-90, -_rb.velocity.normalized.y));
        Destroy(_fire , 6);
    }
}
