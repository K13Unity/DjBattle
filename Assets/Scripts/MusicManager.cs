using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> levelMusicList;
    [SerializeField] private AudioSource audioSource;
    public static MusicManager instance { get; private set; }
    private int _currentlvl = 0;
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

    public void PlayLevelMusic(int lvlNumber)
    {
        if(lvlNumber ==_currentlvl) return; 
        _currentlvl = lvlNumber;

            if (lvlNumber - 1 < levelMusicList.Count && lvlNumber - 1 >= 0)
            {
                audioSource.clip = levelMusicList[lvlNumber - 1];
                audioSource.Play();
            }
    }
}
