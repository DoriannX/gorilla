using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fire;
    private GameObject _currentButton;

    private void Start()
    {
        _currentButton = GameObject.Find("Balanced");
    }
    public void EasySelectionned()
    {
        _currentButton = GameObject.Find("Easy");
        levelManager._difficulty = 0;
    }

    public void BalancedSelectionned()
    {
        _currentButton = GameObject.Find("Balanced");
        levelManager._difficulty = 1;
    }

    public void HardSelectionned()
    {
        _currentButton = GameObject.Find("Hard");
        levelManager._difficulty = 2;
    }

    private void Update()
    {
        if (_currentButton != null)
        {
            _fire.gameObject.SetActive(true);
            _fire.gameObject.transform.position = new Vector3(_currentButton.transform.position.x, _currentButton.transform.position.y);
        }
        else
        {
            _fire.gameObject.SetActive(false);
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
