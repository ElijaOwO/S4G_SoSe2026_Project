using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int attackDamage;
    [SerializeField] private float lifeTime;
    
    private Transform target;
    
    public Transform Target { get => target; set => target = value; }

    private void Start()
    {
        transform.LookAt(target);
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (target != null)
        {
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<BasicPlayer>().Hit(attackDamage);
        }
        Destroy(gameObject);
    }
}
