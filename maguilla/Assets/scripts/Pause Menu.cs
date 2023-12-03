using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fire;
    private GameObject _currentButton;
    [SerializeField] private GameObject _resumeButton, _closeButton, _mainMenu;

    private void Awake()
    {
        _currentButton = _resumeButton;
        EventSystem.current.SetSelectedGameObject(_resumeButton);
    }

    public void CloseSelectionned()
    {
        _currentButton = _closeButton;
        EventSystem.current.SetSelectedGameObject(_closeButton);
    }

    public void MainSelectionned()
    {
        _currentButton = _mainMenu;
        EventSystem.current.SetSelectedGameObject(_mainMenu);
    }

    public void ResumeSelectionned()
    {
        _currentButton = _resumeButton;
        EventSystem.current.SetSelectedGameObject(_resumeButton);
    }

    public void ResumeGame()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        Cursor.visible = gameObject.activeSelf;
        if (gameObject.activeSelf) Time.timeScale = 0;
        else
        {
            Time.timeScale = 1;
            _fire.gameObject.SetActive(false);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Cursor.visible = true;
    }

    private void Update()
    {
        _currentButton = EventSystem.current.currentSelectedGameObject;
        if (_currentButton != null)
        {
            _fire.gameObject.SetActive(true);
            _fire.gameObject.transform.position = new Vector3(_currentButton.transform.position.x, _currentButton.transform.position.y, _fire.gameObject.transform.position.z);
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

    public GameObject Current()
    {
        return _currentButton;
    }
}
