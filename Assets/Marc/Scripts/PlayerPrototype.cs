// Author: MS
// edited by: JET
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerPrototype : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    public CharacterController controller;
    [SerializeField] private MarcPlayerUIController  uiController;

    [SerializeField] private List<LevelExitScript> exits = new List<LevelExitScript>();
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private GameObject playerSpriteObject;

    [SerializeField] private int hp;

    private float currentSpeedMultiplier = 1.0f;
    private bool isDashing = false;
    private bool canDash = true;
    private bool canAttack = true;
    private bool isAttacking = false;
    private bool iFrames = false;

    private List<GameObject> enemys = new List<GameObject>();
    private MyTimer attackCoolDownTimer = new MyTimer();
    
    public  bool IsAttacking { get { return isAttacking; } }
    public  bool IFrames { get { return iFrames; } }
    public int Hp { get { return hp; } }

    
    void Update()
    {
        Move();
        
        attackCoolDownTimer.Update(Time.deltaTime);
    }

    private void Move()
    {
        Vector2 direction = inputHandler.Direction;

        float horizontal = direction.x;
        float vertical = direction.y;

        Vector3 velocity = new Vector3(horizontal, 0, vertical).normalized;
        controller.Move(velocity * (Time.deltaTime * (playerData.speed * currentSpeedMultiplier)));
    }

    public void OnDash()
    {
        if (!playerData.canDashDuringAttack && isAttacking)
        {
            return;
        }
        else if (!isDashing && canDash)
        {
            if(playerData.toggleInstantSnapDash)
            {
                PerformInstantSnapDash();
            }
            else
            {
                StartCoroutine(PerformDash());
            }
            
            StartCoroutine(DashTimer());              
        }
    }

    public void OnAttack()
    {
        if (attackCoolDownTimer.TimeOut())
        {
            if (enemys.Count > 0)
            {
                enemys[0].gameObject.GetComponent<Enemy>().Hit(playerData.attackDamage);
            }
            
            StartCoroutine(AttackDurationTimer());
            attackCoolDownTimer.Reset();
        }
    }
    
    public void Hit(int damage)
    {
        if (iFrames)
        {
            return;
        }
        
        hp -= damage;
        uiController.UpdateProgressBar(hp);
        activateIFrames();
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    public void activateIFrames()
    {
        if(playerData.toggleIFrames)
        {
            StartCoroutine(StartIFramesDuration());
            StartCoroutine(StartIFramesBlinking());
        }
    }

    private void PerformInstantSnapDash()
    {
        Vector2 inputDirection = inputHandler.Direction;
        Vector3 dashDirection = new Vector3(inputDirection.x, 0, inputDirection.y).normalized;

        if (dashDirection == Vector3.zero)
        {
            return;
        }

        if (playerData.ignoreDamageDuringDash)
        {
            StartCoroutine(SnapDashIFrames());
        }

        controller.enabled = false;
        transform.position += dashDirection * playerData.snapDashDistance;
        controller.enabled = true;
    }

    private IEnumerator PerformDash()
    {
        yield return new WaitForSeconds(playerData.dashDelay);
        currentSpeedMultiplier = playerData.dashMultiplier;
        isDashing = true;

        if (playerData.ignoreDamageDuringDash)
        {
            iFrames = true;
        }

        yield return new WaitForSeconds(playerData.dashDuration);
        currentSpeedMultiplier = 1.0f;
        isDashing = false;

        if (playerData.ignoreDamageDuringDash)
        {
            iFrames = false;
        }
    }
    
    private IEnumerator AttackDurationTimer()
    {
        isAttacking = true;
        yield return new WaitForSeconds(playerData.attackDuration);
        isAttacking = false;
    }

    private IEnumerator SnapDashIFrames()
    {
        iFrames = true;
        yield return new WaitForSeconds(playerData.dashDuration);
        iFrames = false;
    }

    private IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(playerData.dashDelay);
        canDash = false;
        yield return new WaitForSeconds(playerData.dashCooldown);
        canDash = true;
    }
    
    private IEnumerator StartIFramesDuration()
    {
        iFrames = true;
        yield return new WaitForSeconds(playerData.iFramesDuration);
        iFrames = false;
    }

    private IEnumerator StartIFramesBlinking()
    {
        while (iFrames)
        {
            playerSpriteObject.SetActive(false);
            yield return new WaitForSeconds(playerData.iFramesBlinkSpeed);
            playerSpriteObject.SetActive(true);
            yield return new WaitForSeconds(playerData.iFramesBlinkSpeed);
        }

        playerSpriteObject.SetActive(true);
    }
}
