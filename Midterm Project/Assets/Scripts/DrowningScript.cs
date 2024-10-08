using UnityEngine;

public class Drowner : MonoBehaviour
{
    private bool isActive = true;

    // Call this method to deactivate the NPC
    public void DeactivateNPC()
    {
        if (isActive)
        {
            isActive = false;
            gameObject.SetActive(false); // Deactivate the Drowner
            Invoke(nameof(DrowningRespawn)); // Call DrowningRespawn
        }
    }

    private void DrowningRespawn()
    {
        isActive = true;
        gameObject.SetActive(true); // Reactivate the drowner
        // Need to reset position to random
    }
}