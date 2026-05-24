using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AranaScript : MonoBehaviour
{

    [SerializeField] Transform target;
    NavMeshAgent agent;
    private Animator animator;
    public int knockbackForce;
    public float knockbackDuration;
    public int vida;
    public int DanoInfligido;
    public float velocidad;
    public GameObject[] drops;
    public float probabilidadDrop;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        knockbackForce = 8;
        knockbackDuration = 0.2f;
        DanoInfligido = 5;
        vida = 1;
        velocidad = 6f;
        probabilidadDrop = 0.5f;
        agent.speed = velocidad;
        // Bloqueamos rotaciones 3D para que no se "tuerza" el sprite
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {

        if (target != null)
        {
            agent.SetDestination(target.position);
        }

        // --- EXPLICACIÓN AQUÍ ---
        // agent.velocity nos da la dirección y rapidez actual del enemigo.
        // .normalized hace que el vector tenga longitud 1 (para que no afecte la velocidad al Animator).
        Vector3 direccion = agent.velocity.normalized;

        // Ahora asignamos la dirección del agente a los parámetros del Animator
        animator.SetFloat("MovimientoX", direccion.x);
        animator.SetFloat("MovimientoY", direccion.y);

        // Opcional: Para que el animador sepa si se está moviendo o no (Speed)
        // animator.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BalaRevolver"))
        {
            Destroy(collision.gameObject);
            vida--;
            muelto();
        }


        if (collision.gameObject.CompareTag("Player"))
        {
            ControlJugador player = collision.gameObject.GetComponent<ControlJugador>();

            if (player != null)
            {
                player.Knockback(transform, knockbackForce, knockbackDuration, DanoInfligido);
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("BalaEscopeta") || other.gameObject.CompareTag("Explosion") || other.gameObject.CompareTag("ExplosionPequena") || other.gameObject.CompareTag("BalaEscopetaEspecial"))
        {
            if (SpawnerEnemigos.Instancia != null)
            {
                SpawnerEnemigos.Instancia.ReducirContadorEnemigos();
            }
            Destroy(gameObject);
        }
    }

    public void muelto()
    {
        if (vida <= 0) { 
            if (UnityEngine.Random.value <= probabilidadDrop && drops.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, drops.Length);
                Instantiate(drops[index], transform.position, Quaternion.identity);
            }
            if (SpawnerEnemigos.Instancia != null)
            {
                SpawnerEnemigos.Instancia.ReducirContadorEnemigos();
            }
            Destroy(gameObject);
        }
    }

    public void settarget(Transform neotarget)
    {
        target = neotarget;

        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (target != null && agent != null)
        {
            agent.SetDestination(target.position);
        }
    }
}