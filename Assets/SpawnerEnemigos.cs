using System.Collections;
using TMPro;
using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{
    public static SpawnerEnemigos Instancia;
    [SerializeField] public GameObject zombie;
    [SerializeField] public GameObject zombiefuerte;
    [SerializeField] public GameObject arana;
    [SerializeField] public GameObject lobo;
    [SerializeField] private float zombiIntervalo = 3.5f;
    [SerializeField] private float zombifuerteIntervalo = 4f;
    [SerializeField] private float aranaIntervalo = 1.8f;
    [SerializeField] private float loboIntervalo = 5f;
    public int puntuacion;
    public TextMeshProUGUI puntuacionTexto;
    [SerializeField] Transform jugador;
    public int contadorenemigos;

    void Awake()
    {
        Instancia = this;
    }

    void Start()
    {
        contadorenemigos = 0;
        StartCoroutine(spawnEnemies(zombiIntervalo, zombie));
        StartCoroutine(spawnEnemies(zombifuerteIntervalo, zombiefuerte));
        StartCoroutine(spawnEnemies(aranaIntervalo, arana));
        StartCoroutine(spawnEnemies(loboIntervalo, lobo));
    }

    void Update()
    {
        puntuacion = int.Parse(puntuacionTexto.text);
    }

    private IEnumerator spawnEnemies(float intervalo, GameObject enemigo)
    {
        yield return new WaitForSeconds(intervalo);

        if (contadorenemigos >= 1000)
        {
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(spawnEnemies(intervalo, enemigo));
            yield break;
        }

        GameObject nuevoEnemigo = Instantiate(enemigo, new Vector3(Random.Range(-5f,5), Random.Range(-6f,6), 0), Quaternion.identity);
        contadorenemigos++;
        enemigo scriptEnemigo = nuevoEnemigo.GetComponent<enemigo>();
        if (scriptEnemigo != null) { scriptEnemigo.settarget(jugador); }
        AranaScript scriptArana = nuevoEnemigo.GetComponent<AranaScript>();
        if (scriptArana != null) { scriptArana.settarget(jugador); }
        ZombieFuerte scriptZombiFuerte = nuevoEnemigo.GetComponent<ZombieFuerte>();
        if (scriptZombiFuerte != null) { scriptZombiFuerte.settarget(jugador); }
        Lobo scriptLobo = nuevoEnemigo.GetComponent<Lobo>();
        if (scriptLobo != null) { scriptLobo.settarget(jugador); }
        StartCoroutine(spawnEnemies(intervalo, enemigo));
    }

    public void ReducirContadorEnemigos()
    {
        contadorenemigos--;
        if (contadorenemigos < 0) { contadorenemigos = 0; }
    }
}
