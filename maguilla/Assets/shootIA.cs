using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class shootIA : MonoBehaviour
{
    private float _timer = 0;
    [SerializeField] private GameObject _projectile;
    private Transform _transform;

    private void Awake()
    {
        _transform =transform;
    }
    private void Update()
    {
        if (_timer < 3)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _timer = 0;
            GameObject _ball = Instantiate(_projectile, _transform.position, Quaternion.identity);
            _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1) * 10;
        }
    }
}
