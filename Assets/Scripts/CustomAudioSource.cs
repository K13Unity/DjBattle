using System;
using UnityEngine;

public class CustomAudioSource : MonoBehaviour
{
    private AudioSource audioSource;
    public bool isPlaying;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);  // «берегти об'Їкт при рестарт≥ сцени
        PlayMusic();
    }

    void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void Play()
    {
        throw new NotImplementedException();
    }

    internal void GetSpectrumData(float[] spectrumData, int v, FFTWindow blackman)
    {
        throw new NotImplementedException();
    }
}
