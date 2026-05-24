using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public Image imagenCorazon;
    public Sprite corazonNormal;
    public Sprite corazonMedio;
    public Sprite corazonCritico;
    private SpriteRenderer spriteRenderer;
    private Color colorGrisParpadeo;
    private float tiempoTranscurrido;
    private bool partidaActiva;

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
        tiempoTranscurrido = 0f;
        partidaActiva = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (partidaActiva)
        {
            tiempoTranscurrido += Time.deltaTime;
        }

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

        if (Input.GetKeyDown(KeyCode.Space)) { Vida -= 30; ActualizarUIVida(); }

        //Game Over
        if (Vida<=0)
        {
            partidaActiva = false;
            //Time.timeScale = 0;
            //Debug.Log("colapso");
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

    //especial azul
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ArmaEspecial1"))
        {
            int arma = GetComponentInChildren<Disparar>().Getarma();
            if (arma == 1)//revolver especial
            {
                GetComponentInChildren<Disparar>().RevolverEspecial2();
            }
            else if (arma == 2)//escopeta especial
            {
                GetComponentInChildren<Disparar>().EscopetaEspecial2();
            }
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

        if (other.CompareTag("pocion1"))
        {
            curar(15);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("pocion2"))
        {
            curar(100);
            Destroy(other.gameObject);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        //hacer que cuando la explosion le toque pare de bajarle vida y se haga invulnerable
        if (other.CompareTag("Explosion"))
        {
            if (RecibirDano==false) { return; }
            Vida -= 10;
            InvulnerabilidadEXP2();
        }
        else if (other.CompareTag("ExplosionPequena"))
        {
            if (RecibirDano == false) { return; }
            Vida -= 7;
            InvulnerabilidadEXP2();
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
        LayerMask LayerExplosion = LayerMask.GetMask("Explosion1");
        LayerMask LayerZombies = LayerMask.GetMask("Zombie1");
        rb.excludeLayers = LayerZombies | LayerExplosion;
        Vector2 direccion = (transform.position - enemigoTransform.position).normalized;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direccion * knockbackForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockbackDuration);
        rb.linearVelocity = Vector2.zero;
        isKnockedBack = false;
        yield return new WaitForSeconds(3.0f);
        rb.excludeLayers = 0;
        RecibirDano = true;
    }

    public void InvulnerabilidadEXP2()
    {
        StartCoroutine(InvulnerabilidadEXP1());
    }

    private IEnumerator InvulnerabilidadEXP1()
    {
        RecibirDano = false;
        ActualizarUIVida();
        LayerMask LayerExplosion = LayerMask.GetMask("Explosion1");
        LayerMask LayerZombies = LayerMask.GetMask("Zombie1");
        rb.excludeLayers = LayerZombies | LayerExplosion;
        yield return new WaitForSeconds(3.0f);
        rb.excludeLayers = 0;
        RecibirDano = true;
    }

    public bool PuedeMoverse()
    {
        return !isKnockedBack;
    }

    public void curar(int curarvalor)
    {
        if (Vida<100)
        {
            for (int i=1; i< curarvalor; i++)
            {
                if (Vida == 100)
                    break;
                Vida++;
            }
        }
        ActualizarUIVida();
    }

    public void ActualizarUIVida()
    {
        TextoVida.text = Vida+"%";
        Color color100 = Color.white;
        Color color50 = new Color(1f, 0.5f, 0f); //naranja
        Color color0 = new Color(0.5f, 0f, 0f); //rojo
        Color colorFinal;

        if (Vida > 50)
        {
            float t = (Vida - 50f) / 50f; // de 0 a 1
            colorFinal = Color.Lerp(color50, color100, t);
        }
        else
        {
            float t = Vida / 50f; // de 0 a 1
            colorFinal = Color.Lerp(color0, color50, t);
        }

        TextoVida.color = colorFinal;

        if (imagenCorazon != null)
        {
            if (Vida <= 25f)
                imagenCorazon.sprite = corazonCritico;
            else if (Vida <= 50f)
                imagenCorazon.sprite = corazonMedio;
            else
                imagenCorazon.sprite = corazonNormal;
        }
    }
}