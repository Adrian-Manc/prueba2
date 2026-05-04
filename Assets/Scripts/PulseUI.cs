using UnityEngine;

public class PulseUI : MonoBehaviour
{
    public float speed = 2f;          // Velocidad base
    public float scaleAmount = 0.2f;  // Escala base

    public VidaJugador vidaJugador;   // Referencia al script de vida

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Seguridad por si no asignas la referencia
        if (vidaJugador == null)
            return;

        float porcentaje = vidaJugador.ObtenerPorcentajeVida();

        float velocidadActual = speed;
        float escalaActual = scaleAmount;

        // Ajustes según vida
        if (porcentaje <= 25f)
        {
            velocidadActual = speed * 3f;      // Muy rápido
            escalaActual = scaleAmount * 1.5f; // Late más fuerte
        }
        else if (porcentaje <= 50f)
        {
            velocidadActual = speed * 2f;      // Medio
            escalaActual = scaleAmount * 1.2f;
        }

        float scale = 1 + Mathf.Sin(Time.time * velocidadActual) * escalaActual;
        transform.localScale = originalScale * scale;
    }
}