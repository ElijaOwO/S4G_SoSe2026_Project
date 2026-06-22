// Author: MS
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
                PlayerPrototype player = other.GetComponent<PlayerPrototype>();
                player.Hit(damage);
        }
    }
}
