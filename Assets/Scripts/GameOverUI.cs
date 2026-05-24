using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public GameObject panelGameOver;

    public TextMeshProUGUI textoPuntuacionFinal;
    public TextMeshProUGUI textoTiempoFinal;

    public TimerUI timerUI;
    public TextMeshProUGUI textoPuntuacionHUD; // el de gameplay (opcional)

    public void MostrarGameOver()
    {
        if (panelGameOver != null)
            panelGameOver.SetActive(true);

        // PUNTUACIÓN (desde HUD o puedes cambiarlo luego a int)
        if (textoPuntuacionFinal != null)
        {
            textoPuntuacionFinal.text = "Puntuación: " +
                (textoPuntuacionHUD != null ? textoPuntuacionHUD.text : "0");
        }

        // TIEMPO
        if (textoTiempoFinal != null && timerUI != null)
        {
            textoTiempoFinal.text = "Tiempo: " + timerUI.ObtenerTiempoFormateado();
        }
    }
}