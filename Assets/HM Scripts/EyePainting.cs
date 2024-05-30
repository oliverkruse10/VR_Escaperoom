using UnityEngine;

public class EyePainting : MonoBehaviour, IHittable
{
    public GameObject FireBoy; 
    public TaskCompletionSound taskCompletionSound; 

    public void OnHit()
    {
        // Spiller lyden når opgaven er løst
        if (taskCompletionSound != null)
        {
            taskCompletionSound.PlayCompletionSound();
        }

        // Får fat i FireBoy objektet og fjerner det
        if (FireBoy != null)
        {
            Destroy(FireBoy);
        }
    }
}
