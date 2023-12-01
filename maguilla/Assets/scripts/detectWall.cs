using UnityEngine;

public class detectWall : MonoBehaviour
{
    private bool _wallInFront = false;


    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "wall")
        {
            _wallInFront = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "wall")
        {
            _wallInFront = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "wall")
            _wallInFront = false;
    }

    public bool is_wall_in_front()
    {
        return _wallInFront;
    }

    public void set_wall_in_front(bool state)
    {
        _wallInFront = state;
    }
}
