using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class addforce : MonoBehaviour
{
    private GameObject _shooter;
    [SerializeField] private float _angle;
    [SerializeField] private float _force;
    private Vector2 _direction;
    private Vector3 _position;
    private void Awake()
    {
        _shooter = GameObject.Find("shooter");
        _position = _shooter.transform.position;
        var rb = GetComponent<Rigidbody2D>();
        var player = GameObject.Find("player");
        var mc = player.GetComponent<moveCharacter>();
        var s = _shooter.GetComponent<shoot>();
        if (s.getShooting())
        {
            _direction = new Vector2(1, 1);
            if (mc.getDirection() != Vector2.zero)
            {
                if (mc.getDirection().x <= 1 && mc.getDirection().x >= 0 && mc.getDirection().y <= 1 &&
                    mc.getDirection().y >= 0)
                {
                    _direction = mc.getDirection();
                    _direction.x = math.clamp(mc.getDirection().x, 0, 1);
                    _direction.y = math.clamp(mc.getDirection().y, 0, 1);
                }
            }
        }
        else
        {
            _direction = new Vector2(mc.getMousePos().x - _position.x, mc.getMousePos().y - _position.y) * 0.5f;
        }
        

        if (_angle < 0) _angle = 0;
        if (_angle > 90) _angle = 90;

        if (_force < 0) _force = 0;
        if (_force > 100) _force = 90;

        rb.velocity += _direction*_force;
    }
}
