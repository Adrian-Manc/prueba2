using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;   // Prefab de la bala
    public Transform firePoint;       // Punto desde donde dispara
    public float bulletSpeed = 10f;

    public int maxAmmo = 1000;
    private int currentAmmo;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Dispara con clic IZQUIERDO
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("¡Sin munición!");
            return;
        }

        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("Falta asignar Bullet Prefab o FirePoint en el Inspector.");
            return;
        }

        currentAmmo--;

        // Posición del mouse en mundo
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // Dirección hacia el clic
        Vector2 direction = (mousePos - firePoint.position).normalized;

        // Crear la bala
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Aplicar velocidad
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
    }
}
