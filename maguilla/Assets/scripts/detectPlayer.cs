using UnityEngine;

public class detectPlayer : MonoBehaviour
{
    private bool _playerDetected;
    private Transform _transform;
    private destroy _destroy;
    private shakeCamera _shakeCamera;
    [SerializeField] private ParticleSystem _particleSystem;

    private void Awake()
    {
        _transform = transform;
        _destroy = GetComponent<destroy>();
        _shakeCamera = Camera.main.GetComponent<shakeCamera>().GetComponent<shakeCamera>();
    }

    private void Update()
    {
        if (_destroy.is_landed())
        {
            Instantiate(_particleSystem, _transform.position, Quaternion.identity);
            Destroy(gameObject);
            _shakeCamera.Start();
        }
    }
}
