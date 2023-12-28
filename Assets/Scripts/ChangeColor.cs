using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Color[] targetColors; // Масив HDR кольорів
    public float lerpSpeed = 0.1f;
    public float intensity = 10f; // Інтенсивність

    private int currentIndex = 0;
    private Material material;

    void Start()
    {
        // Отримайте матеріал кулі
        material = GetComponent<Renderer>().material;

        // Включити емісію на матеріалі
        material.EnableKeyword("_EMISSION");

        // Перевірте, чи є хоча б один цільовий колір у масиві
        if (targetColors != null && targetColors.Length > 0)
        {
            // Встановіть початковий колір емісії з інтенсивністю
            material.SetColor("_EmissionColor", targetColors[0] * intensity);
        }
    }

    void Update()
    {
        // Лінійне зміщення кольору емісії від поточного до наступного цільового кольору
        Color currentColor = material.GetColor("_EmissionColor");
        Color targetEmissionColor = targetColors[currentIndex] * intensity;
        Color lerpedColor = Color.Lerp(currentColor, targetEmissionColor, lerpSpeed * Time.deltaTime);

        // Застосуйте вагові коефіцієнти для зменшення різкості переходу
        float smoothFactor = 0.9f; // Зменшення різкості переходу
        lerpedColor = Color.Lerp(currentColor, lerpedColor, smoothFactor);

        material.SetColor("_EmissionColor", lerpedColor);

        // Якщо досягнуто деякого ступеня наближення до цільового кольору, змініть індекс
        if (Vector4.Distance(lerpedColor, targetEmissionColor) < 0.01f)
        {
            currentIndex = (currentIndex + 1) % targetColors.Length;
        }
    }
}
