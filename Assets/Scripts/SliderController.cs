using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    [SerializeField] private List<Image> image;
    [SerializeField] private Sprite red;
    [SerializeField] private Sprite white;
    [SerializeField] private int _value;

    public int value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            Display();
        }
    }

    public int maxValue => image.Count;

    void Start()
    {
        Display();
    }
    
    private void Display()
    {
        for (int i = 0; i < image.Count; i++)
        {
            if (i < value)
            {
                image[i].sprite = red;
            }
            else
            {
                image[i].sprite = white;
            }
        }
    }
}
