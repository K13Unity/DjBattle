using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    public List<Image> image = new List<Image>();
    public Sprite red;
    public Sprite white;
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            value--;
            Display();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            value++;
            Display();
        }
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
