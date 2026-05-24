using UnityEngine;

public class Revolver : MonoBehaviour
{
    private new Rigidbody2D rb2d;
    public float velocidad = 20f;
    void Start()
    {
        rb2d=GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(transform.position+ transform.up * velocidad*Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Disparar disparo = GameObject.FindWithTag("Puntero").GetComponent<Disparar>();
        disparo.Puntuacion(collision.gameObject);
        if (collision.CompareTag("zombie") || collision.CompareTag("zombiefuerte") || collision.CompareTag("arana") || collision.CompareTag("lobo") || collision.CompareTag("Muro")|| collision.CompareTag("lapida"))
        {
            Destroy(gameObject);
        }
    }
}
