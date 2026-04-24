using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    void Start()
{
    Debug.Log("MainMenu activo");
}
    public void PlayGame()
    {
        Debug.Log("Click detectado");
        SceneManager.LoadScene("SampleScene"); // nombre de tu escena de juego
    }

    public void QuitGame()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }
}