using System.Collections.Generic;
using UnityEngine;

public class lapida : MonoBehaviour
{
    [SerializeField] private ParticleSystem psExplosion;
    private bool psActivo=false;

    void Start()
    {
        
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
            Destroy(gameObject, 1.5f);
        }
        if (collision.gameObject.CompareTag("BalaEscopeta"))
        {
            psExplosion.Play();
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            Collider2D collider = GetComponent<Collider2D>();
            Destroy(sprite);
            Destroy(collider);
            Destroy(gameObject, 1.5f);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("BalaEscopeta"))
        {
            psExplosion.Play();
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            Collider2D collider = GetComponent<Collider2D>();
            Destroy(sprite);
            Destroy(collider);
            Destroy(gameObject, 1.5f);
        }
    }
}
