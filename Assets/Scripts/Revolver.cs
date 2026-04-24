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
        if (collision.CompareTag("Enemigo"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Muro"))
        {
            Destroy(gameObject);
        }
    }
}
