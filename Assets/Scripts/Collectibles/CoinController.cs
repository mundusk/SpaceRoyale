using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] float lifespan = 3f;

    void Update()
    {
        if(lifespan > 0f)
            lifespan -= Time.deltaTime;
        else
            Destroy(gameObject);        
    }
}
