using UnityEngine;

public class PulseUI : MonoBehaviour
{
    public float speed = 2f;        // Velocidad del efecto
    public float scaleAmount = 0.2f; // Cuánto crece (0.2 = 20%)

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * speed) * scaleAmount;
        transform.localScale = originalScale * scale;
    }
}