// Author: JET
using UnityEngine;
using UnityEngine.AI;


public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyHealthComponent healthComponent;
    [SerializeField] protected EnemyUI enemyUI;
    [SerializeField] protected PlayerPrototype target;
    
    protected NavMeshAgent agent;
    protected float distance;
    protected int currentHp;

    abstract public void Hit(int damage);

    abstract protected void Die();
}
