using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VidaJugador : MonoBehaviour
{
    public float vidaMaxima = 100f;
    public float vidaActual;

    public TMP_Text textoVida;
    public Image imagenCorazon;

    public Sprite corazonNormal;
    public Sprite corazonMedio;
    public Sprite corazonCritico;

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarUI();
    }

    public void RecibirDaño(float daño)
    {
        vidaActual -= daño;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        ActualizarUI();
    }

    void ActualizarUI()
    {
        float porcentaje = (vidaActual / vidaMaxima) * 100f;

// Texto %
textoVida.text = Mathf.RoundToInt(porcentaje) + "%";

// Colores base
Color color100 = Color.white;       // blanco
Color color50 = new Color(1f, 0.5f, 0f);  // naranja
Color color0 = new Color(0.5f, 0f, 0f);       // rojo oscuro

Color colorFinal;

// Interpolación
if (porcentaje > 50f)
{
    float t = (porcentaje - 50f) / 50f; // de 0 a 1
    colorFinal = Color.Lerp(color50, color100, t);
}
else
{
    float t = porcentaje / 50f; // de 0 a 1
    colorFinal = Color.Lerp(color0, color50, t);
}

textoVida.color = colorFinal;

// ❤️ CAMBIO DE CORAZÓN
if (imagenCorazon != null)
{
    if (porcentaje <= 25f)
        imagenCorazon.sprite = corazonCritico;
    else if (porcentaje <= 50f)
        imagenCorazon.sprite = corazonMedio;
    else
        imagenCorazon.sprite = corazonNormal;
}
    }

    

    void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        RecibirDaño(10f);
    }
}
public float ObtenerPorcentajeVida()
{
    return (vidaActual / vidaMaxima) * 100f;
}
}