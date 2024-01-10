using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private List<AudioSource> audioSources;

    private void Start()
    {
        SetVolumeForAllSources(0.5f); // Встановлюємо початковий об'єм для всіх аудіоджерел
    }

    private void Update()
    {
        float volume = PlayerPrefs.GetFloat("Volume");
        SetVolumeForAllSources(volume); // Встановлюємо об'єм для всіх аудіоджерел згідно зі значенням в PlayerPrefs
    }

    private void SetVolumeForAllSources(float volume)
    {
        {
            AudioSource[] audioSources = GetComponents<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                if (audioSource != null)
                {
                    audioSource.volume = volume;
                }
                else
                {
                    Debug.LogWarning("One of the AudioSource components is null.");
                }
            }
        }

    }
}
