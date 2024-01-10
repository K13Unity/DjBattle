using UnityEngine;
using UnityEngine.UI;

public class SliderMusic : MonoBehaviour
{
    public Slider slider;
    public float oldVolume;

    private void Start()
    {
        oldVolume = PlayerPrefs.GetFloat("Volume", 0.5f);
        slider.value = oldVolume; 
    }

    private void Update()
    {
        if (oldVolume != slider.value)
        {
            PlayerPrefs.SetFloat("Volume", slider.value);
            PlayerPrefs.Save();
            oldVolume = slider.value;
        }
    }
}
