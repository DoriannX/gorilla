using UnityEngine;

public class IAanimation : MonoBehaviour
{
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;
    private Vector3 _posOrigin = Vector3.zero;
    private Vector3 _tempPos = Vector3.zero;
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rbParent;

    private void Awake()
    {
        _transform = transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rbParent = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        _tempPos = Vector3.zero;
        _tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * _frequency) * _amplitude;
        _transform.localPosition = Vector3.up * 0.3f + _tempPos;
        _spriteRenderer.flipX = (_rbParent.velocity.x < 0.1f);
    }
}
