// Author: MS
using UnityEngine;

public class DestroyableProp : MonoBehaviour
{
    [SerializeField] private int propHealth = 3;

    private bool isColliding = false;
    private bool hasBeenHit = false;
    
    private PlayerPrototype player = null;

    private void Update()
    {
        if (player != null)
        {
            if (isColliding && player.IsAttacking && !hasBeenHit)
            {
                propHealth--;
                hasBeenHit = true;

                Debug.Log("Health: " + propHealth);

                if(propHealth <= 0)
                {
                    Destroy(gameObject);
                }
            }
            
            if (!player.IsAttacking)
            {
                hasBeenHit = false;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = true;
            if (player == null)
            {
                player = other.gameObject.GetComponent<PlayerPrototype>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = false;
        }
    }
}
