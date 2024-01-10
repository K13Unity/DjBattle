using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; private set; }
    
    [SerializeField] SliderController slider;
    [SerializeField] Image winImage;
    [SerializeField] private Text win1;
    [SerializeField] private Text win2;
    private int _win1Count;
    private int _win2Count;
    private int score = 6;
    private bool canTriggerWinEvents = true;
    private GameController _gameController;
    private int _currentWin1Count;
    private int _currentWin2Count;
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
    public int GetScore()
    {
        return score;
    }

    public void UpdateScoreText(int value)
    {
        score += value;
        slider.value = score;

        if (slider.value <= 0 && canTriggerWinEvents)
        {
            canTriggerWinEvents = false;
            _win1Count++;
            _currentWin1Count++;
            win1.text = _win1Count.ToString();
            StartCoroutine(PlayWin());
            StartCoroutine(CheckWinConditions());
            _gameController.PlayerDestroy(PlayerTypes.RIGHT);
        }

        if (slider.value >= slider.maxValue && canTriggerWinEvents)
        {
            canTriggerWinEvents = false;
            _win2Count++;
            _currentWin2Count++;
            win2.text = _win2Count.ToString();
            StartCoroutine(PlayWin());
            StartCoroutine(CheckWinConditions());
            _gameController.PlayerDestroy(PlayerTypes.LEFT);
        }
    }
    public void Init(GameController controller)
    {
        _gameController = controller;
    }
    private IEnumerator PlayWin()
    {
        yield return new WaitForSeconds(3.0f);
        winImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        canTriggerWinEvents = true;
        winImage.gameObject.SetActive(false);
        score = 6;
        slider.value = score;
    }
    private IEnumerator CheckWinConditions()
    {
        if (_currentWin1Count > 0 && _currentWin1Count %2 == 0)
        {
            yield return new WaitForSeconds(3.0f);
            _gameController.TransitionToNextLevel();
            _currentWin1Count = 0;
        }
        else if (_currentWin2Count > 0 && _currentWin2Count %2 == 0)
        {
            yield return new WaitForSeconds(3.0f);
            _gameController.TransitionToNextLevel();
            _currentWin2Count = 0;
        }

    }
}
