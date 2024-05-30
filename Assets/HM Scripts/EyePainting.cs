using UnityEngine;

public class EyePainting : MonoBehaviour, IHittable
{
    public GameObject FireBoy; 
    public TaskCompletionSound taskCompletionSound; 

    public void OnHit()
    {
        // Spiller lyden n�r opgaven er l�st
        if (taskCompletionSound != null)
        {
            taskCompletionSound.PlayCompletionSound();
        }

        // F�r fat i FireBoy objektet og fjerner det
        if (FireBoy != null)
        {
            Destroy(FireBoy);
        }
    }
}
