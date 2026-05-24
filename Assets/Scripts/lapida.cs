using System.Collections.Generic;
using UnityEngine;

public class lapida : MonoBehaviour
{
    [SerializeField] private ParticleSystem psExplosion;
    private bool psActivo=false;
    public GameObject[] drops;
    public float probabilidadDrop;

    void Start()
    {
        probabilidadDrop = 0.8f;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BalaRevolver"))
        {
            psExplosion.Play();
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            Collider2D collider = GetComponent<Collider2D>();
            Destroy(sprite);
            Destroy(collider);
            if (UnityEngine.Random.value <= probabilidadDrop && drops.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, drops.Length);
                Instantiate(drops[index], transform.position, Quaternion.identity);
            }
            Destroy(gameObject, 1.5f);
        }
        if (collision.gameObject.CompareTag("BalaEscopeta") || collision.gameObject.CompareTag("BalaEscopetaEspecial"))
        {
            psExplosion.Play();
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            Collider2D collider = GetComponent<Collider2D>();
            Destroy(sprite);
            Destroy(collider);
            if (UnityEngine.Random.value <= probabilidadDrop && drops.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, drops.Length);
                Instantiate(drops[index], transform.position, Quaternion.identity);
            }
            Destroy(gameObject, 1.5f);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("BalaEscopeta") || other.gameObject.CompareTag("BalaEscopetaEspecial") || other.gameObject.CompareTag("Explosion"))
        {
            psExplosion.Play();
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            Collider2D collider = GetComponent<Collider2D>();
            Destroy(sprite);
            Destroy(collider);
            if (UnityEngine.Random.value <= probabilidadDrop && drops.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, drops.Length);
                Instantiate(drops[index], transform.position, Quaternion.identity);
            }
            Destroy(gameObject, 1.5f);
        }
    }
}
