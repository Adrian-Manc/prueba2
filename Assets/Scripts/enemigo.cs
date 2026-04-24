using UnityEngine;

public class enemigo : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Disparo"))
        {
            Destroy(gameObject);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);
    }
}
