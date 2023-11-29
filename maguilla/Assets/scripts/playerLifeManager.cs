using UnityEngine;
using UnityEngine.UI;

public class playerLifeManager : MonoBehaviour
{
    private float _life = 100;
    [SerializeField] private Slider _slider;
    
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
            this.enabled = false;
            Time.timeScale = 0;

        }
    }
}
