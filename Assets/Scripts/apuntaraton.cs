using UnityEngine;

public class ApuntaRaton : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // Convertimos la posición del ratón al mundo 2D
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Nos aseguramos de estar en el plano 2D

        // Calculamos la dirección desde el objeto hacia el ratón
        Vector2 direccion = (mousePos - transform.position).normalized;

        // Hacemos que el eje "arriba" (el eje Y del objeto) mire hacia la dirección
        // Esto es ideal porque el cono de partículas suele disparar hacia el eje Y local
        transform.up = direccion;
    }
}