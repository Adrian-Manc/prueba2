using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    [SerializeField] public float speed; // The speed at which the player moves
    public bool canMoveDiagonally = true; // Controls whether the player can move diagonally
    private float MovimientoX;
    private float MovimientoY;
    private Animator animator;
    public Disparar disparar;
    private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to the player
    [SerializeField] private Vector2 movement; // Stores the direction of player movement
    private bool isMovingHorizontally = true; // Flag to track if the player is moving horizontally
    public int Vida;
    public TextMeshProUGUI TextoVida;
    public bool RecibirDano;
    public bool isKnockedBack;
    private ControlJugador playerControler;

    void Start()
    {
        // Initialize the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        // Prevent the player from rotating
        //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator = GetComponent<Animator>();
        Vida = 100;
        playerControler = GetComponent<ControlJugador>();
        RecibirDano= true;
    }

    /*void Update()
    {
        if (!playerControler.PuedeMoverse()) return;
        // Get player input from keyboard or controller
        MovimientoX = Input.GetAxisRaw("Horizontal");
        MovimientoY = Input.GetAxisRaw("Vertical");
        animator.SetFloat("MovimientoX", MovimientoX);
        animator.SetFloat("MovimientoY", MovimientoY);
        // Check if diagonal movement is allowed
        if (MovimientoX!=0||MovimientoY!=0)
        {
            animator.SetFloat("UltimoX", MovimientoX);
            animator.SetFloat("UltimoY", MovimientoY);
        }

        movement = new Vector2(MovimientoX, MovimientoY).normalized;
        /*if (canMoveDiagonally)
        {
            // Set movement direction based on input
            movement = new Vector2(MovimientoX, MovimientoY).normalized;
            // Optionally rotate the player based on movement direction
            //RotatePlayer(horizontalInput, verticalInput);
        }
        else
        {
            // Determine the priority of movement based on input
            if (MovimientoX != 0)
            {
                isMovingHorizontally = true;
            }
            else if (MovimientoY != 0)
            {
                isMovingHorizontally = false;
            }

            // Set movement direction and optionally rotate the player
            if (isMovingHorizontally)
            {
                movement = new Vector2(MovimientoX, 0);
                //RotatePlayer(horizontalInput, 0);
            }
            else
            {
                movement = new Vector2(0, MovimientoY);
                //RotatePlayer(0, verticalInput);
            }
        }
    }*/

    void Update()
    {
        // Si está bajo efectos de knockback, limpiamos el vector de movimiento 
        // para que no intente caminar en FixedUpdate.
        if (!playerControler.PuedeMoverse())
        {
            movement = Vector2.zero;
            return;
        }

        // Get player input from keyboard or controller
        MovimientoX = Input.GetAxisRaw("Horizontal");
        MovimientoY = Input.GetAxisRaw("Vertical");
        animator.SetFloat("MovimientoX", MovimientoX);
        animator.SetFloat("MovimientoY", MovimientoY);

        // Check if diagonal movement is allowed
        if (MovimientoX != 0 || MovimientoY != 0)
        {
            animator.SetFloat("UltimoX", MovimientoX);
            animator.SetFloat("UltimoY", MovimientoY);
        }

        movement = new Vector2(MovimientoX, MovimientoY).normalized;

        //Game Over
        if (Vida<=0)
        {
            
        }
    }

    void FixedUpdate()
    {
        // SI PUEDE MOVERSE: Controlamos la velocidad directamente con las teclas
        if (playerControler.PuedeMoverse())
        {
            rb.linearVelocity = movement * speed;
        }
        // SI NO PUEDE MOVERSE (Knockback): No hacemos NADA aquí. 
        // Dejamos que la fuerza del AddForce actúe libremente en el Rigidbody2D.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ArmaEspecial1"))
        {
            GetComponentInChildren<Disparar>().RevolverEspecial2();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Bandolera"))
        {
            if (GetComponentInChildren<Disparar>().cargadorRevolver2 < 100)
            {
                GetComponentInChildren<Disparar>().cargadorRevolver2 += Random.Range(5, 31);
            }
            if (GetComponentInChildren<Disparar>().cargadorEscopeta2 < 100)
            {
                GetComponentInChildren<Disparar>().cargadorEscopeta2 += Random.Range(1, 11);
            }
            GetComponentInChildren<Disparar>().ActualizarUI();
            Destroy(other.gameObject);
        }
    }

    void RotatePlayer(float x, float y)
    {
        // If there is no input, do not rotate the player
        if (x == 0 && y == 0) return;

        // Calculate the rotation angle based on input direction
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        // Apply the rotation to the player
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Knockback(Transform enemigoTransform, int knockbackForce, float knockbackDuration, int DanoEnemigo)
    {
        if (!RecibirDano)
        {
            return;
        }
        if (RecibirDano)
        {
            if (isKnockedBack)
            {
                return;
            }
            StartCoroutine(KnockbackRoutine(enemigoTransform, knockbackForce, knockbackDuration, DanoEnemigo));
        }
    }

    private IEnumerator KnockbackRoutine(Transform enemigoTransform, int knockbackForce, float knockbackDuration, int DanoEnemigo)
    {
        isKnockedBack = true;
        Vida-= DanoEnemigo;
        RecibirDano = false;
        ActualizarUIVida();
        Vector2 direccion = (transform.position - enemigoTransform.position).normalized;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direccion * knockbackForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockbackDuration);
        rb.linearVelocity = Vector2.zero;
        isKnockedBack = false;
        yield return new WaitForSeconds(3.0f);
        RecibirDano = true;
    }

    public bool PuedeMoverse()
    {
        return !isKnockedBack;
    }

    public void ActualizarUIVida()
    {
        TextoVida.text = Vida+"%";
    }
}
