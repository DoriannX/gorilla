using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{

    private float _playerLife;
    private float _IAlife;
    private playerLifeManager _plm;
    private IAlifeManager _IAlm;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    private void Awake()
    {
        _plm = GameObject.Find("player").GetComponent<playerLifeManager>();
        _IAlm = GameObject.Find("IA").GetComponent<IAlifeManager>(); 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

    }

    private void Update()
    {
        
        _playerLife = _plm.Life();
        _IAlife = _IAlm.Life();
    }


    public void restart(InputAction.CallbackContext ctx)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
