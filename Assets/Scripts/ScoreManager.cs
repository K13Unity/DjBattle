using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; private set; }
    
    [SerializeField] SliderController slider;
    [SerializeField] Image winImage;
    [SerializeField] private Text leftPlayerScore;
    [SerializeField] private Text rightPlayerScore;
    private int leftPlayerWinCount;
    private int rightPlayerWinCount;
    private int curentScore = 6;
    private GameController _gameController;
    private int _currentLeftPlayerWinCount;
    private int _currentRightPlayerWinCount;
    [SerializeField] float winAnimationDelay = 3.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateScoreText(int value)
    {
        curentScore += value;
        slider.value = curentScore;

        if (slider.value <= 0 )
        {
            leftPlayerWinCount++;
            _currentLeftPlayerWinCount++;
            leftPlayerScore.text = leftPlayerWinCount.ToString();
            StartCoroutine(PlayWin());
            StartCoroutine(CheckWinConditions());
            _gameController.PlayerDestroy(PlayerTypes.RIGHT);
        }

        if (slider.value >= slider.maxValue)
        {
            rightPlayerWinCount++;
            _currentRightPlayerWinCount++;
            rightPlayerScore.text = rightPlayerWinCount.ToString();
            StartCoroutine(PlayWin());
            StartCoroutine(CheckWinConditions());
            _gameController.PlayerDestroy(PlayerTypes.LEFT);
        }
    }

    public void SetGameController(GameController controller)
    {
        _gameController = controller;
    }

    private IEnumerator PlayWin()
    {
        yield return new WaitForSeconds(winAnimationDelay);
        winImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(winAnimationDelay);
        winImage.gameObject.SetActive(false);
        curentScore = 6;
        slider.value = curentScore;
    }

    private IEnumerator CheckWinConditions()
    {
        if (_currentLeftPlayerWinCount > 0 && _currentLeftPlayerWinCount % 2 == 0)
        {
            yield return new WaitForSeconds(winAnimationDelay);
            _gameController.TransitionToNextLevel();
            _currentLeftPlayerWinCount = 0;
        }
        else if (_currentRightPlayerWinCount > 0 && _currentRightPlayerWinCount % 2 == 0)
        {
            yield return new WaitForSeconds(winAnimationDelay);
            _gameController.TransitionToNextLevel();
            _currentRightPlayerWinCount = 0;
        }

    }
}
