// Author: JET
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicPlayer : MonoBehaviour
{
    [SerializeField] private BasicPlayerUIController  uiController;
    [SerializeField] private BasicPlayerInputHandler inputHandler;
    [SerializeField] private GameObject visuals;
    [SerializeField] private Transform cameraTf;
    [SerializeField] private CharacterController cc;
    [SerializeField] private List<LevelExitScript> exits = new List<LevelExitScript>();
    [SerializeField] private int hp = 15;
    
    [Header("ATTACK")]
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackCooldown = 1;
    
    [Header("SPEED")]
    [SerializeField] private float moveSpeed = 20;
    
    public int Hp { get => hp; }
    private List<GameObject> enemys = new List<GameObject>();
    private MyTimer attackCoolDownTimer = new MyTimer();

    private void Start()
    {
        attackCoolDownTimer.maxTime = attackCooldown;
    }

    private void FixedUpdate()
    {
        Move();
        attackCoolDownTimer.Update(Time.deltaTime);
    }

    private void Move()
    {
        Vector3 moveDir;
        
        Vector2 direction = inputHandler.Direction;
        float horizontal = direction.x;
        float vertical = direction.y;
        
        Vector3 camForward = cameraTf.forward;
        Vector3 camRight = cameraTf.right;
        camForward.y = 0;
        camRight.y = 0;
 
        Vector3 forwardRelativeToCam = vertical * camForward;
        Vector3 RightRelativeToCam = horizontal * camRight;
        moveDir = (forwardRelativeToCam + RightRelativeToCam).normalized;
        
        if(moveDir != Vector3.zero)
        {
            visuals.transform.rotation = Quaternion.LookRotation(moveDir);
        }
        
        cc.Move(moveDir * (moveSpeed * Time.deltaTime));
       // transform.Translate(moveDir * (moveSpeed * Time.deltaTime));
    }

    public void Hit(int damage)
    {
        hp -= damage;
        uiController.UpdateProgressBar(hp);
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnAttack()
    {
        if (enemys.Count > 0 && attackCoolDownTimer.TimeOut())
        {
            enemys[0].gameObject.GetComponent<Enemy>().Hit(attackDamage);
            attackCoolDownTimer.Reset();
        }
    }
    
    private void OnTriggerEnter (Collider other) 
    {
        
        Debug.Log(other.gameObject);
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
