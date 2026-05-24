using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI textoTiempo;

    private float tiempoSegundos;
    private bool activo = true;

    void Update()
    {
        if (!activo) return;

        tiempoSegundos += Time.deltaTime;

        int minutos = Mathf.FloorToInt(tiempoSegundos / 60f);
        int segundos = Mathf.FloorToInt(tiempoSegundos % 60f);

        textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    public void DetenerTiempo()
    {
        activo = false;
    }

    public void ReiniciarTiempo()
    {
        tiempoSegundos = 0f;
        activo = true;
    }

    public float ObtenerTiempo()
    {
        return tiempoSegundos;
    }

    public string ObtenerTiempoFormateado()
{
    int minutos = Mathf.FloorToInt(tiempoSegundos / 60f);
    int segundos = Mathf.FloorToInt(tiempoSegundos % 60f);

    return string.Format("{0:00}:{1:00}", minutos, segundos);
}

}