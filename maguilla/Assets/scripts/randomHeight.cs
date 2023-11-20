using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class randomHeight : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform childTransform in transform)
        {
            // Déplacez chaque enfant par rapport au parent
            Vector2 newScale = new Vector2(2.875f, Random.Range(1, 9));
            childTransform.localScale = (Vector3)newScale;
        }
    }

    void FixedUpdate()
    {
        
    }
}
