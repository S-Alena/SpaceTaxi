using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeHandler : MonoBehaviour
{
    public Button volumeToggle;
    public GameObject volumeSlider;
    public AudioSource bgMusic, sfx;
    Slider slider;

    private float value = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.SetActive(false);
        bgMusic.volume = value;
        sfx.volume = value + 0.2f;

        slider = volumeSlider.GetComponent<Slider>();
        slider.value = value;

        Button volumeB = volumeToggle.GetComponent<Button>();
        volumeB.onClick.AddListener(ToggleVolumeSlider);
    }

    public void Update()
    {
        value = slider.value;
        bgMusic.volume = value;
        if(value + 0.2f <= 1)
        {
            sfx.volume = value + 0.2f;
        }
        else
        {
            sfx.volume = 1;
        }
    }

    public void ToggleVolumeSlider()
    {
        if (volumeSlider.active == true)
        {
            volumeSlider.SetActive(false);
        }
        else
        {
            volumeSlider.SetActive(true);
        }
    }

}
