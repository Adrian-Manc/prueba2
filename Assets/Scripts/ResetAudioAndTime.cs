using UnityEngine;

public class ResetAudioAndTime : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}