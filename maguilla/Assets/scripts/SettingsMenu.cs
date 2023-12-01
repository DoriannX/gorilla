using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fire;

    [SerializeField] private GameObject _easyButton,
        _balancedButton,
        _hardButton,
        _closeButton,
        _startButton;
    private GameObject _currentButton, _currentDifficulty;

    private void Awake()
    {
        setDifficulty();
    }

    public void setDifficulty()
    {
        switch (levelManager._difficulty)
        {
            case 0: EventSystem.current.SetSelectedGameObject(_easyButton); _currentButton = _easyButton; _currentDifficulty = _easyButton; break;
            case 1: EventSystem.current.SetSelectedGameObject(_balancedButton); _currentButton = _balancedButton; _currentDifficulty = _balancedButton; break;
            default: EventSystem.current.SetSelectedGameObject(_hardButton); _currentButton = _hardButton; _currentDifficulty = _hardButton; break;
        }
        
    }
    public void EasySelectionned()
    {
        _currentButton = _easyButton;
        _currentDifficulty = _easyButton;
        EventSystem.current.SetSelectedGameObject(_easyButton);
        levelManager._difficulty = 0;
    }

    public void BalancedSelectionned()
    {
        _currentButton = _balancedButton;
        _currentDifficulty = _balancedButton;
        EventSystem.current.SetSelectedGameObject(_balancedButton);
        levelManager._difficulty = 1;
    }

    public void HardSelectionned()
    {
        _currentButton = _hardButton;
        _currentDifficulty = _hardButton;
        EventSystem.current.SetSelectedGameObject(_hardButton);
        levelManager._difficulty = 2;
    }

    public void CloseSelectionned()
    {
        _currentButton = _closeButton;
        EventSystem.current.SetSelectedGameObject(_closeButton);
    }

    public void ResumeGame()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        Cursor.visible = gameObject.activeSelf;
        if (gameObject.activeSelf) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Cursor.visible = true;
    }

    public void Back()
    {
        EventSystem.current.SetSelectedGameObject(_startButton);
    }

    private void Update()
    {
        _currentButton = EventSystem.current.currentSelectedGameObject;

        if (_currentButton != null)
        {
            _fire.gameObject.SetActive(true);
            _fire.gameObject.transform.position = new Vector3(_currentDifficulty.transform.position.x, _currentDifficulty.transform.position.y);
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public GameObject Current()
    {
        return _currentButton;
    }
}
