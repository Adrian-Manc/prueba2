using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private Camera mainCamera;
    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private float vel = 3.5f;
    [SerializeField] private float distanciaFreno = 0.2f;

    void Start()
    {
        vel = 3.5f;
        distanciaFreno = 0.2f;
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    /*void Update()
    {
        Vector2 targetPosition = obtenerposicionraton();

        // 1. Calculamos la diferencia entre el ratón y el jugador
        Vector2 diferencia = targetPosition - (Vector2)transform.position;

        // 2. Si la distancia es muy pequeña, dejamos de movernos (evita vibraciones)
        if (diferencia.magnitude > 0.1f)
        {
            Vector2 direccion = diferencia.normalized;
            animator.SetFloat("MovimientoX", direccion.x);
            animator.SetFloat("MovimientoY", direccion.y);
            animator.SetFloat("UltimoX", direccion.x);
            animator.SetFloat("UltimoY", direccion.y);
            followmousedelay(targetPosition);
        }
        else
        {
            animator.SetFloat("MovimientoX", 0);
            animator.SetFloat("MovimientoY", 0);
        }
    }

    private void followmousedelay(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, vel * Time.deltaTime);
    }

    private Vector2 obtenerposicionraton()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }*/
    void Update() // Las físicas se mueven mejor en FixedUpdate
    {
        Vector2 targetPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 diferencia = targetPosition - rb.position;

        if (diferencia.magnitude > distanciaFreno)
        {
            Vector2 direccion = diferencia.normalized;

            // Movemos usando la velocidad del Rigidbody en lugar de la posición
            rb.linearVelocity = direccion * vel;

            // Animaciones
            ActualizarAnimaciones(direccion);
        }
        else
        {
            // Frenado en seco al llegar al ratón
            rb.linearVelocity = Vector2.zero;
            ActualizarAnimaciones(Vector2.zero);
        }
    }

    void ActualizarAnimaciones(Vector2 dir)
    {
        animator.SetFloat("MovimientoX", dir.x);
        animator.SetFloat("MovimientoY", dir.y);

        if (dir.magnitude > 0)
        {
            animator.SetFloat("UltimoX", dir.x);
            animator.SetFloat("UltimoY", dir.y);
        }
    }
}