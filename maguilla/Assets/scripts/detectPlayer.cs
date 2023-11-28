using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectPlayer : MonoBehaviour
{
    private bool _playerDetected;
    private Transform _transform;
    [SerializeField] private ParticleSystem _particleSystem;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (GetComponent<destroy>().is_landed())
        {
            Instantiate(_particleSystem, _transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        
    }
}
