using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    // Public variables
    [SerializeField] public float speed; // The speed at which the player moves
    public bool canMoveDiagonally = true; // Controls whether the player can move diagonally
    private float MovimientoX;
    private float MovimientoY;
    private Animator animator;

    // Private variables 
    private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to the player
    [SerializeField] private Vector2 movement; // Stores the direction of player movement
    private bool isMovingHorizontally = true; // Flag to track if the player is moving horizontally

    void Start()
    {
        // Initialize the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        // Prevent the player from rotating
        //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
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
        }*/
    }

    void FixedUpdate()
    {
        // Apply movement to the player in FixedUpdate for physics consistency
        //rb.linearVelocity = movement * speed;
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ArmaEspecial1"))
        {
            GetComponentInChildren<Disparar>().RevolverEspecial2();
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
}
