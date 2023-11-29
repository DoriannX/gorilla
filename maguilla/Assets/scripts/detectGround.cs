using UnityEngine;

public class detectGround : MonoBehaviour
{
    private bool _onGround = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _onGround = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        _onGround = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _onGround = false;
    }

    public bool isOnGround()
    {
        return _onGround;
    }

}
