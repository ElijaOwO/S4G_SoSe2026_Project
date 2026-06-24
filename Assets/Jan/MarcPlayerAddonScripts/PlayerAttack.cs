using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Collider attackCollider;
    [SerializeField] private Image attackMarker;
    [SerializeField] private GameObject attackZone;
    
    private bool isAttacking = false;
    private int attackDamage;
    private float attackDuration;
    private float attackCooldown;
    
    private List<LevelExitScript> exits = new List<LevelExitScript>();
    private List<GameObject> enemys = new List<GameObject>();
    private MyTimer attackCoolDownTimer = new MyTimer();
    
    public  bool IsAttacking { get { return isAttacking; } }

    private void Start()
    {
        attackCoolDownTimer.maxTime = attackCooldown;
    }

    private void Update()
    {
        attackCoolDownTimer.Update(Time.deltaTime);
    }

    public void SetValues(int attackDamage, float attackDuration, float attackCooldown, float attackAreaSize, List<LevelExitScript> exits)
    {
        this.attackDamage = attackDamage;
        this.attackDuration = attackDuration;
        this.attackCooldown = attackCooldown;
        this.exits = exits;

        attackCollider.transform.localScale = new Vector3(attackCollider.transform.localScale.x * attackAreaSize,
                                                          attackCollider.transform.localScale.y * 1,
                                                          attackCollider.transform.localScale.z * attackAreaSize);
    }

    public void Attack()
    {
        if (attackCoolDownTimer.TimeOut())
        {
            if (enemys.Count > 0)
            {
                enemys[0].gameObject.GetComponent<Enemy>().Hit(attackDamage);
            }
            
            StartCoroutine(AttackDurationTimer());
            attackCoolDownTimer.Reset();
        }
    }
    
    private void OnTriggerEnter (Collider other) 
    {
        if (!enemys.Contains(other.gameObject) && other.tag == "Enemy")
        {
            enemys.Add(other.gameObject);
        }
    }

    private void OnTriggerExit (Collider other) 
    {
        if (other.tag == "Enemy")
        {
            enemys.Remove(other.gameObject);
        }
    }
    
    private IEnumerator AttackDurationTimer()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
    }

    public void RemoveDeadEnemy(GameObject enemy)
    {
        if(enemys.Contains(enemy))
        {
            enemys.Remove(enemy);
            
            foreach (var exit in exits)
            {
                exit.RemoveEnemy(enemy.GetComponent<Enemy>());
            }
        }
    }
}
