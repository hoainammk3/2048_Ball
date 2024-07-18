using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public AudioSource audioSource;
    
    public AudioClip colSound;
    public AudioClip tranSound;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(Instance);
    }

    public void PlayCollisionSound()
    {
        audioSource.PlayOneShot(colSound);
    }

    public void PlayTransferSound()
    {
        audioSource.PlayOneShot(tranSound);
    }
}