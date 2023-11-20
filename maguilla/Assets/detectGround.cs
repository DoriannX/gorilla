using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class detectGround : MonoBehaviour
{
    private bool OnGround = false;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        OnGround = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        OnGround = false;
    }

    public bool isOnGround()
    {
        return OnGround;
    }

}
