using UnityEngine;

public class DamageObject : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !PlayerMovement.iFrames)
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.activateIFrames();
            }
        }
    }
}
