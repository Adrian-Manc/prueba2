using UnityEngine;
using UnityEngine.UI;

public class OpcionesMenu : MonoBehaviour
{
    [Header("Opciones")]
    public GameObject panelOpciones;

    [Header("Audio")]
    public Slider sliderVolumen;

    void Start()
    {
        float volumenGuardado = PlayerPrefs.GetFloat("Volumen", 1f);

        sliderVolumen.value = volumenGuardado;

        AudioListener.volume = volumenGuardado;
    }

    // Abrir opciones
    public void AbrirOpciones()
    {
        panelOpciones.SetActive(true);
    }

    // Cerrar opciones
    public void VolverAlMenu()
    {
        panelOpciones.SetActive(false);
    }

    // Cambiar volumen
    public void CambiarVolumen(float valor)
    {
        AudioListener.volume = valor;

        PlayerPrefs.SetFloat("Volumen", valor);
    }
}