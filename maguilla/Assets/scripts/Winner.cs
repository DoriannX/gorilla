using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinnerScript : MonoBehaviour
{
    private TextMeshProUGUI _winner;

    private void Awake()
    {
        _winner = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (IAlifeManager._life <= 0)
        {
            _winner.text = "You won";
        }
        else
        {
            _winner.text = "You lose";
        }
    }
}
