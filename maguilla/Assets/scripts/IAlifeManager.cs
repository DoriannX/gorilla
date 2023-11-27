using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAlifeManager : MonoBehaviour
{
    private float _life = 100;

    public void hit(float damage)
    {
        _life -= damage;
        print(_life);
    }
    public float Life()
    {
        return _life;
    }

    private void Update()
    {
        if (_life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
