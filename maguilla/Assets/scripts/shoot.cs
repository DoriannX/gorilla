using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class shoot : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _force;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _trajectoryDot;
    [SerializeField] private GameObject _trajectory;
    [SerializeField] private int _windRange;
    [SerializeField] private float forceMax;
    [SerializeField] private ParticleSystem _fireShoot;
    [SerializeField] private Image _shotIcon;
    [SerializeField] private GameObject _leftArrow;
    [SerializeField] private GameObject _rightArrow;
    [SerializeField] private TextMeshProUGUI _windText;
    [SerializeField] private ParticleSystem _windParticle;
    [SerializeField] private ParticleSystem _fireParticleLeft, _fireParticleHeadLeft, _fireParticleRight, _fireParticleHeadRight;


    private float _wind;
    private Rigidbody2D _rbProjectile;
    private bool _shootingController = false;
    private Transform _transform;
    private moveCharacter _mc;
    private Vector2 _direction;
    private float _timer;
    private Vector2 _positionBullet;
    private Vector2 _previousPositionBullet;
    private Vector2 _speedV;
    private Vector2 _acceleration;
    private GameObject[] _trajectoryDots = new GameObject[100];
    private Mouse mouse;
    private bool _settings;
    private Image _leftArrowImage, _rightArrowImage;
    private moveCharacter _moveCharacter;
    private void Awake()
    {
        _rbProjectile = _projectile.GetComponent<Rigidbody2D>();
        _transform = transform;
        _mc = _player.GetComponent<moveCharacter>();
        _direction = Vector2.zero;
        _timer = 3;
        _positionBullet = _transform.position;
        _previousPositionBullet = _positionBullet;
        _acceleration = new Vector2(_wind, -9.80665f);
        _wind = Random.Range(-_windRange, _windRange);
        for (int i = 0; i < 100; i++)
        {
            _trajectoryDots[i] = Instantiate(_trajectoryDot, _positionBullet, Quaternion.identity, _trajectory.transform);
            _trajectoryDots[i].SetActive(false);
        }
        mouse = Mouse.current;
        _leftArrowImage = _leftArrow.GetComponent<Image>();
        _rightArrowImage = _rightArrow.GetComponent<Image>();
        _moveCharacter = GetComponentInParent<moveCharacter>();
    }

    private void Update()
    {
        _windText.text = Mathf.Abs(_wind).ToString();
        ParticleSystem ps = _windParticle;
        var main = ps.main;
        if (_wind != 0)
        {
            _windParticle.gameObject.SetActive(true);
            main.startSpeedMultiplier = -_wind;
        }
        else
        {
            _windParticle.gameObject.SetActive(false);
        }
        if (_wind > 0)
        {
            _leftArrow.SetActive(true);
            _leftArrowImage.rectTransform.sizeDelta = new Vector2(100 + Mathf.Abs(_wind * 10), _leftArrowImage.rectTransform.sizeDelta.y);
            _rightArrow.SetActive(false);
        }else if (_wind < 0)
        {
            _rightArrow.SetActive(true);
            _rightArrowImage.rectTransform.sizeDelta = new Vector2(100 + Mathf.Abs(_wind * 10), _leftArrowImage.rectTransform.sizeDelta.y);
            _leftArrow.SetActive(false);
        }
        else
        {
            _leftArrow.SetActive(false);
            _leftArrow.SetActive(false);
        }
        _shotIcon.fillAmount += Time.deltaTime / 2;
        if (_timer < 3)
        {
            _fireShoot.gameObject.SetActive(false);
        }
        else
        {
            _fireShoot.gameObject.SetActive(true);
        }

        SetWindToArrow(_fireParticleLeft, _wind, true); SetWindToArrow(_fireParticleRight, _wind, true);
        SetWindToArrow(_fireParticleHeadLeft, _wind, false); SetWindToArrow(_fireParticleHeadRight, -_wind, false);
        _timer += Time.deltaTime;
    }

    public void SetSettings(bool state)
    {
        _settings = state;
    }

    private void SetWindToArrow(ParticleSystem currentArrow, float currentWind, bool rescaling)
    {
        if (rescaling)
        {
            currentArrow.gameObject.transform.localScale = new Vector3(Mathf.Abs(_wind / 10f) + 1, currentArrow.gameObject.transform.localScale.y);
        }
        ParticleSystem.VelocityOverLifetimeModule _fireVelocity = currentArrow.velocityOverLifetime;
        ParticleSystem.MinMaxCurve rate = new ParticleSystem.MinMaxCurve();
        rate.constant = currentWind;
        _fireVelocity.x = rate;
    }

    private void FixedUpdate()
    {
        _acceleration.x = _wind;
        Loop();
    }

    public void changeWind()
    {

        _wind = Random.Range(-_windRange, _windRange);
    }

    private bool Loop()
    {
        if (!_settings)
        {
            if (_moveCharacter.Controller())
            {
                _direction += _moveCharacter.getDirection() * 0.5f;
            }
            else
            {
                _direction += mouse.delta.ReadValue() * 0.1f;
            }
        }
        _direction.x = Mathf.Clamp(_direction.x, 0, forceMax);
        _direction.y = Mathf.Clamp(_direction.y, -1, forceMax);
        var vSpawn = _direction;
;
        _speedV = vSpawn * _force;

        _positionBullet = _transform.position;
        _previousPositionBullet = _positionBullet;
        for (int i = 0; i < 70; i++)
        {
            _previousPositionBullet = _positionBullet;
            _speedV += _acceleration * Time.fixedDeltaTime;
            _positionBullet += _speedV * Time.fixedDeltaTime;
            _trajectoryDots[i/2].SetActive(true);
            _trajectoryDots[i/2].transform.position = _positionBullet;

            RaycastHit2D[] _raycast = Physics2D.CircleCastAll(_positionBullet, 0.1f, Vector2.zero);
            
            foreach (var raycast in _raycast)
            {
                if (raycast.collider.gameObject.tag == "wall")
                {

                    DrawCircleRaycast(_positionBullet);
                    for (int j = i/2; j < _trajectoryDots.Length; j++)
                    {
                        _trajectoryDots[j].SetActive(false);
                    }
                    return false;
                }
            }
        }
        
        return false;
    }

    public float radius = 0.1f; // Le rayon du cercle
    public int numRays = 360; // Le nombre de rayons à dessiner

    // Dessine un cercle de rayons et renvoie tous les objets touchés
    public void DrawCircleRaycast(Vector2 center)
    {

        for (int i = 0; i < numRays; i++)
        {
            float angle = (float)i / numRays * 360f; // Calcule l'angle pour ce rayon
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right; // Calcule la direction du rayon
            
            _direction.x = Mathf.Clamp(_direction.x, 0, forceMax);
            _direction.y = Mathf.Clamp(_direction.y, -1, forceMax);

            // Dessine une ligne du centre du cercle dans la direction du rayon
            Debug.DrawRay(center, direction * radius, Color.green);
        }

    }

    public void ShootPerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _timer >= 2 && !_settings)
        {
            var projectile = Instantiate(_projectile, _transform.position, Quaternion.identity);
            _direction += mouse.delta.ReadValue();
            _direction.x = Mathf.Clamp(_direction.x, 0, forceMax);
            _direction.y = Mathf.Clamp(_direction.y, -1, forceMax);
            projectile.GetComponent<Rigidbody2D>().velocity += _direction * _force;
            _timer = 0;
            _shotIcon.fillAmount = 0;
        }
    }

    public void ShootPerformedController(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            var projectile = Instantiate(_projectile, _transform.position, Quaternion.identity);
            _direction = new Vector2(1, 1);
            _direction.x = Mathf.Clamp(_direction.x, 0, forceMax);
            _direction.y = Mathf.Clamp(_direction.y, -1, forceMax);
            if (_mc.getDirection() != Vector2.zero)
            {
                _direction = _mc.getDirection();
                _direction.x = Mathf.Clamp(_direction.x, 0, 1);
                _direction.y = Mathf.Clamp(_direction.y, 0, 1);
            }

            if (_direction == Vector2.zero)
            {
                _direction = new Vector2(1, 1);
            }
            projectile.GetComponent<Rigidbody2D>().velocity += _direction * _force;
        }
    }

    public bool getShooting()
    {
        return _shootingController;
    }

    public bool Settings()
    {
        return _settings;
    }
    public float Wind()
    {
        return _wind;
    }
}
