using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCompletionSound : MonoBehaviour
{
    public AudioSource audioSource; 

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    
    public void PlayCompletionSound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
       
    }
}

