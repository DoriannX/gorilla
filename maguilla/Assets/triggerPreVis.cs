using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerPreVis : MonoBehaviour
{
    private bool _inGround;

    private void Awake()
    {
        _inGround = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _inGround = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _inGround = false;
    }

    public bool isInGround()
    {
        return _inGround;
    }
}
