using UnityEngine;

public class ApuntaRatonPro : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // 1. Obtener posición del ratón en el mundo
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
        // 2. Calcular dirección desde este objeto al ratón
        Vector2 direccion = (mousePos - transform.position).normalized;

        // 3. Calcular el ángulo en grados
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

        // 4. Aplicar rotación (Restamos 90 si tu partícula sale hacia arriba por defecto)
        transform.rotation = Quaternion.Euler(0, 0, angulo - 90f);
    }
}