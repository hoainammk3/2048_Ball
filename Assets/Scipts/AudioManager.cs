using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public AudioSource soundSource;
    public AudioSource musicSource;
    
    public AudioClip colSound;
    public AudioClip tranSound;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(Instance);
    }

    public void PlayCollisionSound()
    {
        soundSource.PlayOneShot(colSound);
    }

    public void PlayTransferSound()
    {
        soundSource.PlayOneShot(tranSound);
    }

    public void SetVolumeSound(float volume)
    {
        soundSource.volume = volume;
        Debug.Log("sound volume changed: " + volume);
    }

    public void SetVolumeMusic(float volume)
    {
        musicSource.volume = volume;
        Debug.Log("music volume changed: " + volume);
    }
}