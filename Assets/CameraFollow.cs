using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El jugador
    public Vector3 offset;   // Desplazamiento desde el jugador

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
