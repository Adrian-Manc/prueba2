using UnityEngine;

public class Explosivo : MonoBehaviour
{
    [SerializeField] private GameObject caja;
    [SerializeField] private ParticleSystem ps1;
    [SerializeField] private AudioSource explosionSound;
    private bool explotando = false;
    void Start()
    {

    }

    void Update()
    {

    }

    void Explotar()
    {
        explotando = true;
        if (caja != null)
        {
            caja.SetActive(false);
        }
        ps1.Play();
        explosionSound.Play();
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Disparo") && !explotando)
        {
            Explotar();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (!explotando)
        {
            Explotar();
        }
    }

}
