using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fire;
    [SerializeField] private GameObject _startButton, _settingsButton, _quitButton;
    private SettingsMenu _settingsMenu;
    private GameObject _currentButton;

    private void Awake()
    {
        _settingsMenu = _settingsButton.GetComponent<SettingsMenu>();
        _currentButton = _startButton;
        EventSystem.current.SetSelectedGameObject(_startButton);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartSelectionned()
    {
        EventSystem.current.SetSelectedGameObject(_startButton);
    }

    public void SettingsSelectionned()
    {
        EventSystem.current.SetSelectedGameObject(_settingsButton);
    }

    public void QuitSelectionned()
    {
        EventSystem.current.SetSelectedGameObject(_quitButton);
    }

    public void Settings()
    {
        _settingsMenu.setDifficulty();
    }

    private void Update()
    {
        _currentButton = EventSystem.current.currentSelectedGameObject;
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
}
