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
        if (other.gameObject.name == "player")
        {
            _landed = true;
            if (other.gameObject.TryGetComponent<playerLifeManager>(out playerLifeManager temp)) temp.hit(20);
            GameObject.Find("shooter").GetComponent<shoot>().changeWind();
            _sIA.reset_shot();
        }

        if (other.gameObject.name == "IA")
        {
            if (other.gameObject.TryGetComponent<IAlifeManager>(out IAlifeManager tempIA))
            {
                _landed = true;
                tempIA.hit(20);
                GameObject.Find("IA").GetComponent<moveIA>().Move(false);
                GameObject.Find("shooter").GetComponent<shoot>().changeWind();
                _sIA.reset_shot();
            }
        }
    }

    public bool is_landed()
    {
        return _landed;
    }
}
