using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Color[] targetColors; // ����� HDR �������
    public float lerpSpeed = 0.1f;
    public float intensity = 10f; // ������������

    private int currentIndex = 0;
    private Material material;

    void Start()
    {
        // ��������� ������� ���
        material = GetComponent<Renderer>().material;

        // �������� ���� �� �������
        material.EnableKeyword("_EMISSION");

        // ��������, �� � ���� � ���� �������� ���� � �����
        if (targetColors != null && targetColors.Length > 0)
        {
            // ��������� ���������� ���� ��� � ������������
            material.SetColor("_EmissionColor", targetColors[0] * intensity);
        }
    }

    void Update()
    {
        // ˳���� ������� ������� ��� �� ��������� �� ���������� ��������� �������
        Color currentColor = material.GetColor("_EmissionColor");
        Color targetEmissionColor = targetColors[currentIndex] * intensity;
        Color lerpedColor = Color.Lerp(currentColor, targetEmissionColor, lerpSpeed * Time.deltaTime);

        // ���������� ����� ����������� ��� ��������� ������ ��������
        float smoothFactor = 0.9f; // ��������� ������ ��������
        lerpedColor = Color.Lerp(currentColor, lerpedColor, smoothFactor);

        material.SetColor("_EmissionColor", lerpedColor);

        // ���� ��������� ������� ������� ���������� �� ��������� �������, ����� ������
        if (Vector4.Distance(lerpedColor, targetEmissionColor) < 0.01f)
        {
            currentIndex = (currentIndex + 1) % targetColors.Length;
        }
    }
}
