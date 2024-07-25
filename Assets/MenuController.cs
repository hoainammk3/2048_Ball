using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject settingPanel;
    public Slider musicSlider;
    public Slider soundSlider;

    private void Start()
    {
        if (musicSlider)
        {
            musicSlider.value = AudioManager.Instance.GetVolumeMusic();
            musicSlider.onValueChanged.AddListener(delegate {SliderValueChanged("musicSlider");});
        }

        if (soundSlider)
        {
            soundSlider.value = AudioManager.Instance.GetVolumeSound();
            soundSlider.onValueChanged.AddListener(delegate {SliderValueChanged("soundSlider");});
        }
        settingPanel.gameObject.SetActive(false);
    }

    private void SliderValueChanged(string sliderName)
    {
        switch (sliderName)
        {
            case "musicSlider":
                AudioManager.Instance.SetVolumeMusic(musicSlider.value);
                break;
            case "soundSlider":
                AudioManager.Instance.SetVolumeSound(soundSlider.value);
                break;
        }
    }

    public void ShowSettingPanel()
    {
        settingPanel.SetActive(true);
    }

    public void HideSettingPanel()
    {
        settingPanel.SetActive(false);
    }

    public void HandleShowSettingPanel()
    {
        settingPanel.SetActive(!settingPanel.gameObject.activeSelf);
    }
    
    
}
