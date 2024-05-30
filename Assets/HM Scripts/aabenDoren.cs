using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aabenDoren : MonoBehaviour
{
    public GameObject exitDoorLaas; 
    private TaskCompletionSound taskCompletionSound; 

    void Start()
    {
        
        GameObject audioManager = GameObject.Find("AudioManager");
        if (audioManager != null)
        {
            taskCompletionSound = audioManager.GetComponent<TaskCompletionSound>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject == exitDoorLaas)
        {
            
            Destroy(exitDoorLaas);

            
            if (taskCompletionSound != null)
            {
                taskCompletionSound.PlayCompletionSound();
            }
            
        }
    }
}
