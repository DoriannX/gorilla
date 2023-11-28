using UnityEngine;

public class destroy : MonoBehaviour
{
    private Transform _transform;
    private float _timer = 0;
    private GameObject _shooter;
    private shootIA _sIA;
    private bool _landed = false;
    private void Awake()
    {
       _transform = transform;
       _shooter = GameObject.Find("shooter2");
       _sIA = _shooter.GetComponent<shootIA>();
    }

    void Update()
    {
        if (_timer >= 5)
        {
            _landed = true;
            _sIA.reset_shot();
            _timer = 0;
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<CapsuleCollider2D>() != null)
        {
            _landed = true;
            if (other.gameObject.TryGetComponent<playerLifeManager>(out playerLifeManager temp)) temp.hit(20);
            if (other.gameObject.TryGetComponent<IAlifeManager>(out IAlifeManager tempIA))
            {
                print(other.gameObject.name);
                tempIA.hit(20);
                GameObject.Find("IA").GetComponent<moveIA>().Move(false);
            }
            _sIA.reset_shot();
        }
    }

    public bool is_landed()
    {
        return _landed;
    }
}
