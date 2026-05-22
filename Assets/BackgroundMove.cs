using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float speed = 50f;

    public float limitX = 300f;
    public float limitY = 200f;

    public float waveStrength = 0.5f;

    public float changeDirectionTime = 2f;

    private RectTransform rect;
    private Vector2 direction;
    private Vector2 basePosition;

    private float timer;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        basePosition = rect.anchoredPosition;

        direction = Random.insideUnitCircle.normalized;
        timer = changeDirectionTime;
    }

    void Update()
    {
        // 🔹 Cambio de dirección cada X segundos
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Vector2 randomDir = Random.insideUnitCircle.normalized;

            direction = Vector2.Lerp(direction, randomDir, 0.5f).normalized;

            timer = changeDirectionTime;
        }

        // 🔹 Movimiento base
        basePosition += direction * speed * Time.deltaTime;

        // 🔹 Rebote suave en bordes
        if (basePosition.x > limitX || basePosition.x < -limitX)
            direction.x *= -1;

        if (basePosition.y > limitY || basePosition.y < -limitY)
            direction.y *= -1;

        // 🔹 Onda visual (efecto vivo)
        Vector2 waveOffset = new Vector2(
            Mathf.Sin(Time.time * 2f) * waveStrength,
            Mathf.Cos(Time.time * 1.5f) * waveStrength
        );

        rect.anchoredPosition = basePosition + waveOffset;
    }
}