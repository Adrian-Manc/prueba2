using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private float waitTime = 1f;

    void Start()
    {
        StartCoroutine(Intro());
    }

    IEnumerator Intro()
    {
        // Empieza invisible
        canvasGroup.alpha = 0;

        // Fade IN
        float t = 0;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = t / fadeDuration;
            yield return null;
        }

        canvasGroup.alpha = 1;

        // Espera
        yield return new WaitForSeconds(waitTime);

        // Fade OUT
        t = 0;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = 1 - (t / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0;

        // Cargar menú
        SceneManager.LoadScene("Game");
    }
}