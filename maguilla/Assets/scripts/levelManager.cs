using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    private float _playerLife;
    private float _IAlife;
    private playerLifeManager _plm;
    private IAlifeManager _IAlm;
    public static float _difficulty = 1;

    [SerializeField] private GameObject _settingsMenu;

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
        _settingsMenu.SetActive(!_settingsMenu.activeSelf);
        Cursor.visible = _settingsMenu.activeSelf;
        GameObject.Find("shooter").GetComponent<shoot>().SetSettings(_settingsMenu.activeSelf);

    }
}
