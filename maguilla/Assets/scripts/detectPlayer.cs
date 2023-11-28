using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectPlayer : MonoBehaviour
{
    private bool _playerDetected;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (GetComponent<destroy>().is_landed())
        {
            if (other.transform.position.x - _transform.position.x <= .5f &&
                other.transform.position.y - _transform.position.y <= .5f)
            {
                if (other.TryGetComponent<playerLifeManager>(out playerLifeManager temp)) temp.hit(20);
                if (other.TryGetComponent<IAlifeManager>(out IAlifeManager tempIA))
                {
                    tempIA.hit(20);
                    GameObject.Find("IA").GetComponent<moveIA>().Move(false);
                } 
            }
            else if (other.transform.position.x - _transform.position.x <= 1 &&
                other.transform.position.y - _transform.position.y <= 1)
            {
                if (other.TryGetComponent<playerLifeManager>(out playerLifeManager temp)) temp.hit(10);
                if (other.TryGetComponent<IAlifeManager>(out IAlifeManager tempIA))
                {
                    GameObject.Find("IA").GetComponent<moveIA>().Move(false);
                    tempIA.hit(20);
                }
            }
            else
            {
                if (other.TryGetComponent<playerLifeManager>(out playerLifeManager temp)) temp.hit(5);
                if (other.TryGetComponent<IAlifeManager>(out IAlifeManager tempIA))
                {
                    GameObject.Find("IA").GetComponent<moveIA>().Move(false);
                    tempIA.hit(20);
                }
            }
            Destroy(gameObject);
        }
    }
}
