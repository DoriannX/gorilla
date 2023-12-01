using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinnerScript : MonoBehaviour
{
    private TextMeshProUGUI _winner;


    private void Update()
    {
        _winner = GetComponent<TextMeshProUGUI>();
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
