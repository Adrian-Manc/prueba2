using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Lobo : MonoBehaviour
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
    public float velocidad;
    public GameObject[] drops;
    public float probabilidadDrop;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        knockbackForce = 50;
        knockbackDuration = 0.2f;
        DanoInfligido = 18;
        vida = 12;
        velocidad = 3f;
        agent.speed = velocidad;
        // Bloqueamos rotaciones 3D para que no se "tuerza" el sprite
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        RecibirDano = true;
        probabilidadDrop = 0.5f;
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
        if (other.gameObject.CompareTag("BalaEscopeta"))
        {
            if (RecibirDano == false) { return; }
            vida -= 4;
            InvulnerabilidadEXP2();
            InvulnerabilidadEXP2();
            muelto();
        }
        if (other.gameObject.CompareTag("BalaEscopetaEspecial"))
        {
            if (RecibirDano == false) { return; }
            vida -= 8;
            InvulnerabilidadEXP2();
            muelto();
        }
        if (other.gameObject.CompareTag("Explosion"))
        {
            vida -= 12;
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
        {
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
