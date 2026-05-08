using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicaCrossfade : MonoBehaviour
{
    [Header("Canciones")]
    public AudioClip[] canciones;

    [Header("Configuración")]
    public float tiempoCrossfade = 1f;

    [Tooltip("Canciones que deben pasar antes de repetir")]
    public int bloqueoRepeticion = 3;

    private AudioSource sourceA;
    private AudioSource sourceB;

    private AudioSource sourceActual;
    private AudioSource sourceSiguiente;

    private Queue<int> historial = new Queue<int>();

    void Awake()
    {
        // Crear AudioSources
        sourceA = gameObject.AddComponent<AudioSource>();
        sourceB = gameObject.AddComponent<AudioSource>();

        ConfigurarSource(sourceA);
        ConfigurarSource(sourceB);

        sourceActual = sourceA;
        sourceSiguiente = sourceB;

        // Precargar clips en memoria
        foreach (AudioClip clip in canciones)
        {
            if (clip != null)
            {
                clip.LoadAudioData();
            }
        }
    }

    void Start()
    {
        StartCoroutine(ReproducirMusica());
    }

    void ConfigurarSource(AudioSource source)
    {
        source.playOnAwake = false;
        source.loop = false;
        source.volume = 1f;
    }

    IEnumerator ReproducirMusica()
    {
        int indiceActual = ObtenerCancionAleatoria();

        AudioClip clipActual = canciones[indiceActual];

        sourceActual.clip = clipActual;
        sourceActual.volume = 1f;
        sourceActual.Play();

        while (true)
        {
            // Elegir siguiente canción ANTES
            int indiceSiguiente = ObtenerCancionAleatoria();
            AudioClip clipSiguiente = canciones[indiceSiguiente];

            // Precargar clip siguiente
            clipSiguiente.LoadAudioData();

            // Esperar hasta el crossfade
            yield return new WaitForSeconds(
                clipActual.length - tiempoCrossfade
            );

            // Preparar siguiente source
            sourceSiguiente.clip = clipSiguiente;
            sourceSiguiente.volume = 0f;

            // IMPORTANTE:
            // Play ligeramente antes evita microstutter
            sourceSiguiente.Play();

            float t = 0f;

            while (t < tiempoCrossfade)
            {
                t += Time.deltaTime;

                float p = t / tiempoCrossfade;

                sourceActual.volume = 1f - p;
                sourceSiguiente.volume = p;

                yield return null;
            }

            sourceActual.Stop();

            // Intercambiar sources
            AudioSource temp = sourceActual;
            sourceActual = sourceSiguiente;
            sourceSiguiente = temp;

            clipActual = clipSiguiente;
        }
    }

    int ObtenerCancionAleatoria()
    {
        if (canciones.Length == 0)
            return 0;

        List<int> disponibles = new List<int>();

        for (int i = 0; i < canciones.Length; i++)
        {
            if (!historial.Contains(i))
            {
                disponibles.Add(i);
            }
        }

        // Si todas están bloqueadas
        if (disponibles.Count == 0)
        {
            historial.Clear();

            for (int i = 0; i < canciones.Length; i++)
            {
                disponibles.Add(i);
            }
        }

        int elegido = disponibles[
            Random.Range(0, disponibles.Count)
        ];

        historial.Enqueue(elegido);

        while (historial.Count > bloqueoRepeticion)
        {
            historial.Dequeue();
        }

        return elegido;
    }
}