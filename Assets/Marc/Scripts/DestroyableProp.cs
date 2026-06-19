using UnityEngine;

public class DestroyableProp : MonoBehaviour
{
    [SerializeField] private int propHealth = 3;

    private bool isColliding = false;

    private bool hasBeenHit = false;

    private void Update()
    {
        if (isColliding && PlayerMovement.isAttacking && !hasBeenHit)
        {
            propHealth--;
            hasBeenHit = true;

            Debug.Log("Health: " + propHealth);

            if(propHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        if (!PlayerMovement.isAttacking)
        {
            hasBeenHit = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = true;
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
