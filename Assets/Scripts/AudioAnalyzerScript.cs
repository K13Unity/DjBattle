using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioAnalyzerScript : MonoBehaviour
{
    private AudioSource audioSource;
    public float scaleMultiplier = 300f;
    public int bassFrequencyRange = 5;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        AnalyzeAudio();
    }

    private void AnalyzeAudio()
    {
        float[] spectrumData = new float[256];
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Blackman);
        float bassValue = spectrumData[bassFrequencyRange] * scaleMultiplier;
        transform.localScale = Vector3.one + Vector3.one * bassValue;
    }
}