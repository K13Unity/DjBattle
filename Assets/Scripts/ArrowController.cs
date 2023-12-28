using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
    public List<Image> arrowImages = new List<Image>();
    public int maxArrows = 3; // Максимальна кількість стрілок
    private int currentArrows; // Поточна кількість стрілок
    private PlayerController currentPlayer; // Посилання на поточного гравця

    private void Start()
    {
        currentArrows = maxArrows;
        UpdateArrowUI();
    }

    public void AssignPlayer(PlayerController player) => currentPlayer = player;

    public void BulletFired() => UpdateArrowCount(-1);

    public void ReloadArrows() => UpdateArrowCount(1);

    private void UpdateArrowCount(int amount)
    {
        currentArrows = Mathf.Clamp(currentArrows + amount, 0, maxArrows);
        UpdateArrowUI();
    }

    private void UpdateArrowUI()
    {
        for (int i = 0; i < arrowImages.Count; i++)
            arrowImages[i].enabled = i < currentArrows;
    }
}

