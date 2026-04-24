using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        // Ignorar colisión entre la bala y el jugador
        Collider2D bulletCol = GetComponent<Collider2D>();
        Collider2D playerCol = GameObject.Find("Demonio_1").GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(bulletCol, playerCol);

        Destroy(gameObject, 1f); // Se destruye después de 2 segundos
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); // Se destruye al chocar con lo que sea
    }

}
