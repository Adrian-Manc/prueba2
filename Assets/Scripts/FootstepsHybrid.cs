using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepsHybrid : MonoBehaviour
{
    public AudioClip walkingLoop;
    public float minSpeed = 0.1f;
    public float stopDelay = 0.1f; // 🔥 evita cortes por micro-paradas

    private AudioSource audioSource;
    private Vector3 lastPosition;

    private float stopTimer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = walkingLoop;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        lastPosition = transform.position;
    }

    void Update()
    {
        float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;

        bool isMoving = speed > minSpeed;

        if (isMoving)
        {
            stopTimer = 0f;

            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            stopTimer += Time.deltaTime;

            if (stopTimer >= stopDelay)
            {
                if (audioSource.isPlaying)
                    audioSource.Stop();
            }
        }

        lastPosition = transform.position;
    }
}