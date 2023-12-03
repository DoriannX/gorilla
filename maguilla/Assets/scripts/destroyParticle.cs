using UnityEngine;

public class destroyParticle : MonoBehaviour
{   
    private void Awake()
    {
        Destroy(gameObject, 1);
    }
}
