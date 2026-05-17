using System.Collections.Generic;
using UnityEngine;

public class Escopetazo : MonoBehaviour
{
    private ParticleSystem ps;
    private List<ParticleCollisionEvent> collisionEvents;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);
        if (numCollisionEvents > 0)
        {
            Disparar disparo = GameObject.FindWithTag("Puntero").GetComponent<Disparar>();
            disparo.Puntuacion(other);
        }
    }
}
