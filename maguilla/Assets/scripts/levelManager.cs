using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerLifeText;
    [SerializeField] private TextMeshProUGUI _IAlifeText;

    private float _playerLife;
    private float _IAlife;
    private playerLifeManager _plm;
    private IAlifeManager _IAlm;

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
        _playerLifeText.text = _playerLife.ToString();
        _IAlifeText.text = _IAlife.ToString();
    }


    public void restart(InputAction.CallbackContext ctx)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
