using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class enemigo : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    private Animator animator;
    public int knockbackForce;
    public float knockbackDuration;
    public int vida;
    public int DanoInfligido;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        knockbackForce = 20;
        knockbackDuration = 0.2f;
        DanoInfligido = 3;
        vida = 3;
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
            vida--;
            if (vida<=0) {
                Destroy(gameObject);
            }
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
        Destroy(gameObject);
    }

    private void OnParticleTrigger()
    {
        Destroy(gameObject);
    }
}