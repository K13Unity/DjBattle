using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private AudioController controller;
    [SerializeField] private Slider slider;
    private float volume;

    private void Start()
    {
        volume = PlayerPrefs.GetFloat("Volume", 0.5f);
        slider.value = volume;
    }

    
    private void Update()
    {
        if (volume != slider.value)
        {
            PlayerPrefs.SetFloat("Volume", slider.value);
            PlayerPrefs.Save();
            volume = slider.value;
            controller.SetVolumeForAllSources(volume);
        }
    }
}
