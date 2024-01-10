using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private List<AudioSource> audioSources;

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 0.5f);
        SetVolumeForAllSources(volume); 
    }

    public void SetVolumeForAllSources(float volume)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = volume;
        }
    }
}
