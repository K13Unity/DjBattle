using System.Collections;
using UnityEngine;

public class StartRaund : MonoBehaviour
{
    [SerializeField] GameController controller;
    [SerializeField] GameObject video;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        StartCoroutine(Countdown());
    }
    
    IEnumerator Countdown()
    {
        video.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        audioSource.PlayOneShot(audioSource.clip);
        yield return new WaitForSeconds(0.8f);
        video.SetActive(false);
        
        controller.ActivatePlayers();
    }
}
