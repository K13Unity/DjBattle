using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button _start;
    [SerializeField] private Button _settting;
    [SerializeField] private Button _exit;
    [SerializeField] private Button _back;

    public GameObject settingsPanel;
    private void Start()
    {
        settingsPanel.SetActive(false);
        _start.onClick.AddListener(PlayGame);
        _settting.onClick.AddListener(Setting);
        _exit.onClick.AddListener(ExitGame);
    }
    public void PlayGame()
    {
        _start.onClick.RemoveListener(PlayGame); 
        _settting.onClick.RemoveListener(Setting);
        _exit.onClick.RemoveListener(ExitGame);
        _back.onClick.RemoveListener(Back);
        SceneManager.LoadScene("1");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Setting()
    {
       
        settingsPanel.SetActive(true);
        _back.onClick.AddListener(Back);
        _start.onClick.RemoveListener(PlayGame);
        _settting.onClick.RemoveListener(Setting);
        _exit.onClick.RemoveListener(ExitGame);
    }
    public void Back()
    {
        settingsPanel.SetActive(false);
        _start.onClick.AddListener(PlayGame);
        _settting.onClick.AddListener(Setting);
        _exit.onClick.AddListener(ExitGame);
        _back.onClick.RemoveListener(Back);
    }

}