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
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _ia;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    private void Awake()
    {
        _plm = _player.GetComponent<playerLifeManager>();
        _IAlm = _ia.GetComponent<IAlifeManager>(); 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

    }

    private void Update()
    {
        
        _playerLife = _plm.Life();
        _IAlife = _IAlm.Life();
    }
}
