using System.Collections;
using UnityEngine;

public class shakeCamera : MonoBehaviour
{
    private bool _start;
    [SerializeField] private float _duration;
    [SerializeField] private AnimationCurve _curve;

    private void Update()
    {
        if (_start)
        {
            _start = false;
            StartCoroutine(Shaking());
        }
    }
    IEnumerator Shaking()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = _curve.Evaluate(elapsedTime/_duration);
            transform.position = startPos + Random.insideUnitSphere * strength * 5;
            yield return null;
        }
        transform.position = startPos;
    }

    public void Start()
    {
        _start = true;
    }
}
