using UnityEngine;

public class randomHeight : MonoBehaviour
{
    private Transform _transform;
    void Awake()
    {
        _transform = transform;
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
