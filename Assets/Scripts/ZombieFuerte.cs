using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieFuerte : MonoBehaviour
{

    [SerializeField] Transform target;
    NavMeshAgent agent;
    private Animator animator;
    public int knockbackForce;
    public float knockbackDuration;
    public int vida;
    public int DanoInfligido;
    private Rigidbody2D rb;
    public bool RecibirDano;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        knockbackForce = 20;
        knockbackDuration = 0.2f;
        DanoInfligido = 5;
        vida = 10;
        // Bloqueamos rotaciones 3D para que no se "tuerza" el sprite
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        RecibirDano = true;
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
        if (other.gameObject.CompareTag("BalaEscopeta") || other.gameObject.CompareTag("Explosion"))
        {
            if (RecibirDano==false) { return; }
            vida -= 4;
            InvulnerabilidadEXP2();
            muelto();
        }
    }

    public void InvulnerabilidadEXP2()
    {
        StartCoroutine(InvulnerabilidadEXP1());
    }

    private IEnumerator InvulnerabilidadEXP1()
    {
        RecibirDano = false;
        LayerMask LayerExplosion = LayerMask.GetMask("Explosion1");
        LayerMask LayerBalas = LayerMask.GetMask("PlayerBullets");
        rb.excludeLayers = LayerExplosion;
        rb.excludeLayers = LayerBalas;
        yield return new WaitForSeconds(2f);
        rb.excludeLayers = 0;
        RecibirDano = true;
    }

    public void muelto()
    {
        if (vida <= 0)
            Destroy(gameObject);

    }
}
