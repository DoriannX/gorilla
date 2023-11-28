using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLifeManager : MonoBehaviour
{
    private float _life = 100;

    public void hit(float damage)
    {
        _life -= damage;
    }

    public float Life()
    {
        return _life;
    }

    private void Update()
    {
        if (_life <= 0)
        {
            this.enabled = false;
            Time.timeScale = 0;

        }
    }
}
