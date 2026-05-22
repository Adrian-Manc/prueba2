using UnityEngine;

public class LogoPulse : MonoBehaviour
{
    public float speed = 2f;
    public float scaleAmount = 0.05f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float pulse = Mathf.Sin(Time.time * speed);

        float scale = 1 + pulse * scaleAmount;

        transform.localScale = originalScale * scale;
    }
}