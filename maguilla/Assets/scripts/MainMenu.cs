using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fire;
    private GameObject _currentButton;
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
        _currentButton = GameObject.Find("Start Button");
        print(_currentButton.gameObject.name);
    }

    public void SettingsSelectionned()
    {
        _currentButton = GameObject.Find("Settings Button");
        print(_currentButton.gameObject.name);
    }

    public void QuitSelectionned()
    {
        _currentButton = GameObject.Find("Quit Button");
        print(_currentButton.gameObject.name);
    }

    private void Update()
    {
        if (_currentButton != null)
        {
            _fire.gameObject.SetActive(true);
            _fire.gameObject.transform.position = new Vector3(_fire.gameObject.transform.position.x, _currentButton.transform.position.y);
        }
        else
        {
            _fire.gameObject.SetActive(false);
        }
    }
}
