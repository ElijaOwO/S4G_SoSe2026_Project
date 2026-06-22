using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class RangedEnemy : Enemy
{

    [SerializeField] private RangedEnemyData enemyData;
    [SerializeField] private ProjectileSpawner spawner;
    
    private bool isRepositioning = false;
    bool runAwayFromPlayer = false;
    bool isRetreating = false;
    private Vector3 RepositionTraget;

    private MyTimer repositionMyTimer = new MyTimer();
    private MyTimer repositionCoolDownMyTimer = new MyTimer();
    private MyTimer attackCoolDownMyTimer =  new MyTimer();
    private Transform targetTf;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHp = healthComponent.MaxHp;
        targetTf = target.transform;
        repositionMyTimer.maxTime = enemyData.repositionTime;
        repositionCoolDownMyTimer.maxTime = enemyData.repositionCooldown;
        attackCoolDownMyTimer.maxTime = enemyData.attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("AiUpdate", 0, 0.2f);
        
        UpdateTimers();
    }

    private void AiUpdate()
    {
        distance = Vector3.Distance(agent.transform.position, targetTf.position);
        
        if (isRepositioning)
        {
            if (repositionMyTimer.TimeOut())
            {
                StopRepositioning();
            }
            else
            {
                Reposition();
            }
        }
        
        if (distance < enemyData.RepositionDistance || distance > enemyData.attackDistance)
        { 
            if (repositionCoolDownMyTimer.TimeOut() && !isRepositioning)
            {
                Reposition();
                repositionMyTimer.Reset();
            }
        }
        if (distance < enemyData.attackDistance && !isRepositioning)
        {
            agent.isStopped = true;
            Attack();
        }
    }

    private void UpdateTimers()
    {
        repositionMyTimer.Update(Time.deltaTime);
        repositionCoolDownMyTimer.Update(Time.deltaTime);
        attackCoolDownMyTimer.Update(Time.deltaTime);
    }

    private void StopRepositioning()
    {
        
        agent.isStopped = true;
        isRepositioning = false;
        isRetreating = false;
        repositionMyTimer.Reset();
        repositionCoolDownMyTimer.Reset();
    }

    private void Reposition()
    {
        if (distance < enemyData.RepositionDistance || isRetreating)
        {
            agent.isStopped = false;
            MoveAwayFromPlayer();
            isRetreating = true;
        }
        else
        {
            agent.isStopped = false;
            MoveTowardsPlayer();
        }
        
        isRepositioning = true;
    }

    private void MoveAwayFromPlayer()
    {
        Vector3 retreatDirection = (transform.position - targetTf.position);
        agent.SetDestination(retreatDirection);
    }

    private void MoveTowardsPlayer()
    {
        agent.SetDestination(targetTf.position);
    }
    
    private void Attack()
    {
        if (attackCoolDownMyTimer.TimeOut())
        {
            spawner.SpawnProjectile(targetTf);
            attackCoolDownMyTimer.Reset();
        }
    }
    protected override void Die()
    {
        target.RemoveDeadEnemy(gameObject);
        Destroy(gameObject);
    }
    
    public override void Hit(int damage)
    {
        Debug.unityLogger.Log("Hit " + damage);
        currentHp -= damage;
        healthComponent.Updatehealth(damage);
        
        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyData.RepositionDistance);
        Gizmos.color = Color.green;
        
        Gizmos.DrawWireSphere(transform.position, enemyData.attackDistance);
        Gizmos.color = Color.red;
    }
}
