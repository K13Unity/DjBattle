using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public Restart restart;
    public List<PlayerController> players = new List<PlayerController>();

    private PlayerController GetPlayer(PlayerTypes type)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].type == type)
            {
                return players[i];
            }
        }
        return null;
    }
    private void Start()
    {
        ScoreManager.instance.Init(this);
    }
    public void PlayerDestroy(PlayerTypes type)
    {
        var player = GetPlayer(type);
        Destroy(player.gameObject);
        StartCoroutine(AcriveRestartLevel());
    }
    private IEnumerator AcriveRestartLevel()
    {
        yield return new WaitForSeconds(5.0f);
        restart.RestartLevel();
    }
    public void TransitionToNextLevel()
    {
        int currentLevelIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;

        if (nextLevelIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelIndex);
        }
        else
        {
            Debug.LogWarning("No more levels available!");
        }
    }
}
