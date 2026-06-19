using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FastMeleeEnemy : Enemy
{  
    [SerializeField] protected MeleeEnemyData enemyData;
    
    private MyTimer attackCoolDownTimer = new MyTimer();
    private MyTimer attackWindupTimer = new MyTimer();
    private bool isWindingUp = false;
    
     // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start()
     {
         agent = GetComponent<NavMeshAgent>();
         currentHp = healthComponent.MaxHp;
         attackCoolDownTimer.maxTime = enemyData.attackCooldown;
         attackWindupTimer.maxTime = enemyData.attackWindup;
     }
 
     // Update is called once per frame
     // Update is called once per frame
     void Update()
     {
         InvokeRepeating("AiUpdate", 0, 0.2f);
        
         attackCoolDownTimer.Update(Time.deltaTime);
         attackWindupTimer.Update(Time.deltaTime);
     }

     private void AiUpdate()
     {
         distance = Vector3.Distance(agent.transform.position, target.transform.position);
         if (distance < enemyData.attackDistance)
         {
             agent.isStopped = true;
             if (!isWindingUp)
             {
                 InitiateAttack();
             }
         }
         else if (isWindingUp)
         {
             Attack();
         }
         else
         {
             agent.isStopped = false;
             agent.SetDestination(target.transform.position);
         }
     }

     private void Attack()
     {
         if (attackWindupTimer.TimeOut())
         {
             target.Hit(enemyData.attackDamage);
             isWindingUp = false;
             attackCoolDownTimer.Reset();
         }
     }

     private void InitiateAttack()
     {
         if (attackCoolDownTimer.TimeOut())
         {
             if (!isWindingUp)
             {
                 isWindingUp = true;
                 attackWindupTimer.Reset();
             }
         }
     }

     protected override void Die()
     {
         target.RemoveDeadEnemy(gameObject);
         Destroy(gameObject);
     }

    public override void Hit(int damage)
    {
        currentHp -= damage;
        healthComponent.Updatehealth(damage);
        
        if (currentHp <= 0)
        {
            Die();
        }
    }
}
