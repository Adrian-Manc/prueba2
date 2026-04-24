using UnityEngine;

public class RevolverSpecial : MonoBehaviour
{
    private new Rigidbody2D rb2d;
    public float velocidad = 20f;
    [SerializeField] private ParticleSystem ps1;
    [SerializeField] private ParticleSystem ps2;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(transform.position + transform.up * velocidad * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            ps1.transform.position = rb2d.transform.position;
            ps2.transform.position = rb2d.transform.position;
            ps1.Play();
            ps2.Play();
            Destroy(gameObject);

        }
    }
}
