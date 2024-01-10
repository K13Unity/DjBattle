using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public AudioSource audioSource;
    public Light flickerLight;

    public List<Color> flickerColors; // Ліст кольорів для миготіння

    // Задайте інші значення з експериментів та налаштувань.
    public float minIntensity = 1f;
    public float maxIntensity = 5f;
    public float intensityMultiplier = 2f;
    public int bassSpectrumIndex = 2; // Індекс спектра, що відповідає басу.

    void Update()
    {
        // Отримайте значення гучності музики від аудіоспектра.
        float[] spectrum = new float[256];
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        float bassVolume = spectrum[bassSpectrumIndex];

        // Застосуйте значення гучності басу до інтенсивності світла.
        float flickerIntensity = Mathf.Lerp(minIntensity, maxIntensity, bassVolume * intensityMultiplier);
        flickerLight.intensity = flickerIntensity;

        // Змініть колір світла за допомогою ліста кольорів.
        ChangeLightColor();
    }

    void ChangeLightColor()
    {
        int colorIndex = Mathf.FloorToInt(Random.Range(0, flickerColors.Count));
        flickerLight.color = flickerColors[colorIndex];
    }
}
