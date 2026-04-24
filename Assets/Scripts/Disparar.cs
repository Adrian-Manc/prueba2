using System.Collections;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    private float cooldownEscopeta;
    private int contadorbalasEscopeta;
    private float cooldownPistola;
    private int contadorbalasPistola;

    public GameObject prefabBala;
    public GameObject prefabBalaEspecial;
    public Transform puntero;

    [SerializeField] private ParticleSystem psPistola;
    [SerializeField] private ParticleSystem psEscopeta1;
    [SerializeField] private ParticleSystem psEscopeta2;

    [SerializeField] private AudioSource pistolaSound;
    [SerializeField] private AudioSource escopetazoSound;
    [SerializeField] private AudioSource cambioArmaSound;

    // UI armas
    [SerializeField] private RectTransform iconoPistola;
    [SerializeField] private RectTransform iconoEscopeta;

    [SerializeField] private Vector3 tamañoNormal = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 tamañoSeleccionado = new Vector3(1.3f, 1.3f, 1f);

    int arma;
    int armaAnterior;

    void Start()
    {
        arma = 1;
        armaAnterior = arma;

        cooldownEscopeta = 3f;

        // Inicializar UI correctamente
        ActualizarUIArmas();
    }

    void Update()
    {
        // Cambiar arma con tecla (solo cuando se pulsa)
        if (Input.GetKeyDown(KeyCode.Alpha1)) { arma = 1; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { arma = 2; }

        // Detectar cambio de arma
        if (arma != armaAnterior)
        {
            CambiarArma();
            armaAnterior = arma;
        }

        // Disparo
        if (Input.GetMouseButtonDown(0))
        {
            switch (arma)
            {
                case 1:
                    Pistola();
                    break;
                case 2:
                    Escopeta2();
                    break;
            }
        }
    }

    // =========================
    // CAMBIO DE ARMA
    // =========================
    void CambiarArma()
    {
        if (cambioArmaSound != null)
            cambioArmaSound.Play();

        ActualizarUIArmas();
    }

    void ActualizarUIArmas()
    {
        if (iconoPistola == null || iconoEscopeta == null) return;

        // Reset
        iconoPistola.localScale = tamañoNormal;
        iconoEscopeta.localScale = tamañoNormal;

        // Seleccionada
        if (arma == 1)
        {
            iconoPistola.localScale = tamañoSeleccionado;
        }
        else if (arma == 2)
        {
            iconoEscopeta.localScale = tamañoSeleccionado;
        }
    }

    // =========================
    // DISPAROS
    // =========================
    public void Pistola()
    {
        GameObject bala = Instantiate(prefabBala);

        if (pistolaSound != null)
            pistolaSound.Play();

        if (psPistola != null)
            psPistola.Play();

        bala.transform.position = puntero.position;
        bala.transform.rotation = transform.rotation;

        Destroy(bala, 2f);
    }

    public void Escopeta2()
    {
        StartCoroutine(Escopeta1());
    }

    IEnumerator Escopeta1()
    {
        if (psEscopeta1 != null)
            psEscopeta1.Play();

        if (escopetazoSound != null)
            escopetazoSound.Play();

        yield return new WaitForSeconds(0.10f);

        if (psEscopeta2 != null)
            psEscopeta2.Play();
    }

    // =========================
    // ESPECIAL
    // =========================
    public void RevolverEspecial2()
    {
        StartCoroutine(RevolverEspecial1());
    }

    IEnumerator RevolverEspecial1()
    {
        GameObject prefabBalaOriginal = prefabBala;
        prefabBala = prefabBalaEspecial;

        yield return new WaitForSeconds(10f);

        prefabBala = prefabBalaOriginal;
    }
}