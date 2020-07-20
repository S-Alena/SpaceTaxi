using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

using Debug = UnityEngine.Debug;

public class VolumeHandler : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string VolumePref = "VolumePref";
    private int firstPlayInt;
    private float volumeFloat;

    public Button volumeToggle;
    public GameObject volumeSlider;
    public AudioSource bgMusic;
    public AudioSource[] sfx;
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.SetActive(false);
        Button volumeB = volumeToggle.GetComponent<Button>();
        volumeB.onClick.AddListener(ToggleVolumeSlider);

        slider = volumeSlider.GetComponent<Slider>();

        slider = volumeSlider.GetComponent<Slider>();
        volumeFloat = PlayerPrefs.GetFloat(VolumePref);
        slider.value = volumeFloat;
        Debug.Log("More Play detected, setting volumeFloat:" + volumeFloat);

    }
    
    public void Update()
    {
        slider = volumeSlider.GetComponent<Slider>();
        bgMusic.volume = slider.value;
        for(int i = 0; i < sfx.Length; i++)
        {
            if (slider.value + 0.2f <= 1)
            {
                sfx[i].volume = slider.value + 0.2f;
            }
            else
            {
                sfx[i].volume = 1;
            }
        }
        SaveSoundSettings();
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

    public void SaveSoundSettings()
    {
        slider = volumeSlider.GetComponent<Slider>();
        PlayerPrefs.SetFloat(VolumePref, slider.value);
    }

    void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSoundSettings();
        }
    }
}
