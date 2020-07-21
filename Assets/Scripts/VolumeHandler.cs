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
    private float volumeFloat;

    public float fadeDuration;
    public Button volumeToggle;
    public GameObject volumeSlider;
    public AudioSource bgMusic;
    public AudioSource[] sfx;
    Slider slider;
    IEnumerator volumeCoroutine;
    public float targetVolume { get; private set; }

    void Start()
    {
        volumeSlider.SetActive(false);
        Button volumeB = volumeToggle.GetComponent<Button>();
        volumeB.onClick.AddListener(ToggleVolumeSlider);

        volumeFloat = PlayerPrefs.GetFloat(VolumePref, 0.3f);
        slider = volumeSlider.GetComponent<Slider>();
        slider.value = volumeFloat;
        targetVolume = volumeFloat;

        bgMusic.volume = 0f;
        VolumeTransition(targetVolume, fadeDuration, bgMusic);
        for (int i = 0; i < sfx.Length; i++)
        {
            sfx[i].volume = 0f;
            if (targetVolume + 0.2f <= 1)
            {
                VolumeTransition(targetVolume + 0.2f, fadeDuration, sfx[i]);
            }
            else
            {
                VolumeTransition(1f, fadeDuration, sfx[i]);
            }
        }
    }

    public void Update()
    {
        slider = volumeSlider.GetComponent<Slider>();

        bgMusic.volume = slider.value;
        for (int i = 0; i < sfx.Length; i++)
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

    IEnumerator VolumeTransitionCoroutine(float target, float duration, AudioSource audio)
    {
        /*Debug.Log("VolumeTransitionCoroutine aufgerufen.");
        float start = audio.volume;
        float lastTime = 1;
        while (start < target)
        {
            if (Time.time - lastTime > 0.1f)
            {
                audio.volume += (target - start) / duration / 60;
                lastTime = Time.time;
            }

        }
        yield return null;*/
        bgMusic.volume = 0f;
        float progress = 0.0f;
        while (progress < 1.0f && bgMusic.volume < targetVolume)
        {
        progress += Time.deltaTime / duration;
        Debug.Log("progress: " + progress);
        bgMusic.volume += progress;
        Debug.Log("volume: " + bgMusic.volume);
        yield return null;
        }
    }

    public void VolumeTransition(float target, float duration, AudioSource audio)
    {
        Debug.Log("VolumeTransition aufgerufen.");
        targetVolume = target;
        if (volumeCoroutine != null)
        {
            StopCoroutine(volumeCoroutine);
        }
        if (duration <= Mathf.Epsilon && duration > 0)
        {
            audio.volume = target;
        }
        else
        {
            volumeCoroutine = VolumeTransitionCoroutine(target, duration, audio);
            StartCoroutine(volumeCoroutine);
        }
    }
}