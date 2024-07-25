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
    public AudioClip clickSound;
    
    private void Awake()
    {
        Debug.Log("Awake");
        if (!Instance)
        {
            Instance = this;
            soundSource = transform.GetChild(1).GetComponent<AudioSource>();
            musicSource = transform.GetChild(0).GetComponent<AudioSource>();

            colSound = Resources.Load<AudioClip>("Sound/collisionSound");
            tranSound = Resources.Load<AudioClip>("Sound/ballTranferSound");
            clickSound = Resources.Load<AudioClip>("Sound/clickSound");
            DontDestroyOnLoad(gameObject);
        }
//        else
//        {
//            Destroy(gameObject);
//        }
        
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
    }

    public void SetVolumeMusic(float volume)
    {
        musicSource.volume = volume;
    }

    public float GetVolumeSound()
    {
        return soundSource.volume;
    }

    public float GetVolumeMusic()
    {
        return musicSource.volume;
    }

    public void PlayClickSound()
    {
        Debug.Log(soundSource);
        soundSource.PlayOneShot(clickSound);
    }
}