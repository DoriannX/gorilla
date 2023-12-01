using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerLifeManager : MonoBehaviour
{
    private float _life = 100;
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        _life = 100;
    }

    public void hit(float damage)
    {
        _life -= damage;
        
    }

    public float Life()
    {
        return _life;
    }

    private void Update()
    {
        _slider.value = Mathf.Lerp(_slider.value, _life, 0.05f);
        if (_life <= 0)
        {
            
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0, 0.1f);
            if (Time.timeScale <= 0.01f)
            {
                Time.timeScale = 1;
                Cursor.visible = true;
                SceneManager.LoadScene(2);
            }

        }
    }
}
