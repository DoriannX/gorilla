using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectPlayer : MonoBehaviour
{
    private bool _playerDetected;
    private Transform _transform;
    [SerializeField] private ParticleSystem _particleSystem;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (GetComponent<destroy>().is_landed())
        {
            /*if (other.transform.position.x - _transform.position.x <= .5 &&
                other.transform.position.y - _transform.position.y <= .5)
            {

            }
            else if (other.transform.position.x - _transform.position.x <= 1 &&
                other.transform.position.y - _transform.position.y <= 1)
            {
                if (other.TryGetComponent<playerLifeManager>(out playerLifeManager temp)) temp.hit(10);
                if (other.TryGetComponent<IAlifeManager>(out IAlifeManager tempIA))
                {
                    print(other.name);
                    GameObject.Find("IA").GetComponent<moveIA>().Move(false);
                    tempIA.hit(10);
                }
            }
            else if (other.transform.position.x - _transform.position.x <= 2 &&
                     other.transform.position.y - _transform.position.y <= 2)
            {
                if (other.TryGetComponent<playerLifeManager>(out playerLifeManager temp)) temp.hit(5);
                if (other.TryGetComponent<IAlifeManager>(out IAlifeManager tempIA))
                {
                    print(other.name);
                    GameObject.Find("IA").GetComponent<moveIA>().Move(false);
                    tempIA.hit(5);
                }
            }*/

            Instantiate(_particleSystem, _transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
