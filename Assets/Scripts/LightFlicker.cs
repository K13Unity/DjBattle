using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public AudioSource audioSource;
    public Light flickerLight;

    public List<Color> flickerColors; // ˳�� ������� ��� ��������

    // ������� ���� �������� � ������������ �� �����������.
    public float minIntensity = 1f;
    public float maxIntensity = 5f;
    public float intensityMultiplier = 2f;
    public int bassSpectrumIndex = 2; // ������ �������, �� ������� ����.

    void Update()
    {
        // ��������� �������� ������� ������ �� �����������.
        float[] spectrum = new float[256];
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        float bassVolume = spectrum[bassSpectrumIndex];

        // ���������� �������� ������� ���� �� ������������ �����.
        float flickerIntensity = Mathf.Lerp(minIntensity, maxIntensity, bassVolume * intensityMultiplier);
        flickerLight.intensity = flickerIntensity;

        // ����� ���� ����� �� ��������� ���� �������.
        ChangeLightColor();
    }

    void ChangeLightColor()
    {
        int colorIndex = Mathf.FloorToInt(Random.Range(0, flickerColors.Count));
        flickerLight.color = flickerColors[colorIndex];
    }
}
