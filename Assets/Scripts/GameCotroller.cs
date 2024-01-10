using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<PlayerController> players = new List<PlayerController>();
    [SerializeField] private int currentLevel = 1;
    private float _restartDelay = 5f;

    private PlayerController GetPlayer(PlayerTypes type)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].Type == type)
            {
                return players[i];
            }
        }
        return null;
    }
    private void Start()
    {
        ScoreManager.instance.SetGameController(this);
        MusicManager.instance.PlayLevelMusic(currentLevel); 
    }
    public void PlayerDestroy(PlayerTypes type)
    {
        var player = GetPlayer(type);
        Destroy(player.gameObject);
        StartCoroutine(AcriveRestartLevel());
    }
    private IEnumerator AcriveRestartLevel()
    {
        yield return new WaitForSeconds(_restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void TransitionToNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;

        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextLevelIndex);
        }
    }
    
    public void ActivatePlayers()
    {
        for(int i = 0; i < players.Count; i++)
        {
            players[i].SetActive(true);
        }
    }
}
