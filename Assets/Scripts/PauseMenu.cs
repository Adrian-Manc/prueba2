using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject panelPause;
    public Slider sliderVolumen;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // 🔁 ESTE ES EL SISTEMA PRINCIPAL
    public void TogglePause()
    {
        if (isPaused)
            Reanudar();
        else
            Pausar();
    }

    public void Pausar()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        isPaused = true;
    }

    public void Reanudar()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        isPaused = false;
    }

    public void IrAlMenu(string nombreEscena)
    {
        Time.timeScale = 1f;
         AudioListener.pause = false;
        SceneManager.LoadScene("Game");
    }

    public void CambiarVolumen()
    {
        AudioListener.volume = sliderVolumen.value;
    }
}