using System.Collections;
using UnityEngine;
using TMPro;

public class Disparar : MonoBehaviour
{
    public float fireRate1;
    public float fireRate2;
    private float nextFireTime1 = 0f;
    private float nextFireTime2 = 0f;
    public int cargadorRevolver1;
    public TextMeshProUGUI RevolverActualBalas;
    public int cargadorRevolver2;
    public TextMeshProUGUI RevolverRecamaraBalas;
    public int cargadorEscopeta1;
    public TextMeshProUGUI EscopetaActualBalas;
    public int cargadorEscopeta2;
    public TextMeshProUGUI EscopetaRecamaraBalas;
    public int puntuacion;
    public TextMeshProUGUI puntuacionTexto;

    public Rigidbody2D jugador;

    public GameObject prefabBala;
    public GameObject prefabBalaEspecial;
    public Transform puntero;

    [SerializeField] private ParticleSystem psPistola;
    [SerializeField] private ParticleSystem psEscopeta1;
    [SerializeField] private ParticleSystem psEscopeta2;
    [SerializeField] private ParticleSystem psEscopeta1Especial;
    [SerializeField] private ParticleSystem psAuraAzul;

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
        fireRate1 = 0.6f;
        fireRate2 = 1f;
        arma = 1;
        armaAnterior = arma;
        puntuacion = 0;
        cargadorRevolver1 =6;
        cargadorRevolver2 =12;
        ActualizarUI();
        cargadorEscopeta1=2;
        cargadorEscopeta2=6;
        psAuraAzul.Stop();

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

        // Hacer coincidir contador de balas
        ActualizarUI();

        // Disparo
        if (Input.GetMouseButtonDown(0))
        {
            switch (arma)
            {
                case 1:
                    if (Time.time >= nextFireTime1)
                    {
                        if (cargadorRevolver1>0)
                        {
                            Pistola();
                        }
                        else
                        {
                            PistolaRecarga();
                        }
                        
                    }
                    break;
                case 2:
                    if (Time.time >= nextFireTime2)
                    {
                        if (cargadorEscopeta1>0)
                        {
                            Escopeta2();
                        }
                        else
                        {
                            EscopetaRecarga();
                        }
                        
                    }
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
    // RECARGA DE ARMA
    // =========================
    public void ActualizarUI()
    {
        RevolverActualBalas.text = cargadorRevolver1.ToString();
        RevolverRecamaraBalas.text = cargadorRevolver2.ToString();
        EscopetaActualBalas.text = cargadorEscopeta1.ToString();
        EscopetaRecamaraBalas.text = cargadorEscopeta2.ToString();
    }
    public void PistolaRecarga()
    {
        for (int i=0; i<6;i++)
        {
            if (cargadorRevolver2==0)
            {
                break;
            }
            cargadorRevolver1++;
            cargadorRevolver2--;
            if (cargadorRevolver2==0)
            {
                break;
            }
        }
    }

    public void EscopetaRecarga()
    {
        for (int i = 0; i < 2; i++)
        {
            if (cargadorEscopeta2 == 0)
            {
                break;
            }
            cargadorEscopeta1++;
            cargadorEscopeta2--;
            if (cargadorEscopeta2 == 0)
            {
                break;
            }
        }
    }

    // =========================
    // DISPAROS
    // =========================
    public void Pistola()
    {
        GameObject bala = Instantiate(prefabBala);
        nextFireTime1 = Time.time + fireRate1;
        if (pistolaSound != null)
            pistolaSound.Play();

        if (psPistola != null)
            psPistola.Play();
        cargadorRevolver1--;
        bala.transform.position = puntero.position;
        bala.transform.rotation = transform.rotation;

        Destroy(bala, 2f);
    }

    public void Escopeta2()
    {
        nextFireTime2 = Time.time + fireRate2;
        StartCoroutine(Escopeta1());
    }

    IEnumerator Escopeta1()
    {
            if (psEscopeta1 != null)
                psEscopeta1.Play();
            cargadorEscopeta1--;
            if (escopetazoSound != null)
                escopetazoSound.Play();

            yield return new WaitForSeconds(0.10f);

            if (psEscopeta2 != null)
                psEscopeta2.Play();
        
    }

    public int Puntuacion(GameObject objeto)
    {
        if (objeto.CompareTag("lapida"))
        {
            puntuacion += 6;
            puntuacionTexto.text = puntuacion.ToString();
        }
        if (objeto.CompareTag("Enemigo"))
        {
            puntuacion += 10;
            puntuacionTexto.text = puntuacion.ToString();
        }

        return 0;
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
        psAuraAzul.Play();

        yield return new WaitForSeconds(10f);

        prefabBala = prefabBalaOriginal;
        psAuraAzul.Stop();
    }



}