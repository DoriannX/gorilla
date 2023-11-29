using UnityEngine;

public class fireParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fire;

    private void Awake()
    {
        _fire = Instantiate(_fire, transform);
    }

    private void Update()
    {
        _fire.transform.rotation = Quaternion.Euler(new Vector2(-90, -GetComponent<Rigidbody2D>().velocity.normalized.y)  );
        Destroy(_fire , 6);
    }
}
