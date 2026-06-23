// Author: MS
// edited by: JET

using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerPrototype : MonoBehaviour
{ 
    public CharacterController controller;
    [SerializeField] private PlayerData playerData; 
    [SerializeField] private MarcPlayerUIController  uiController; 
    [SerializeField] private PlayerAttack playerAttack; 
    
    [SerializeField] private List<LevelExitScript> exits = new List<LevelExitScript>(); 
    [SerializeField] private PlayerInputHandler inputHandler; 
    [SerializeField] private GameObject playerSpriteObject;
    
    [SerializeField] private int hp;
    
    private float currentSpeedMultiplier = 1.0f; 
    private bool isDashing = false; 
    private bool canDash = true; 
    private bool iFrames = false; 
    
    public  bool IsAttacking { get { return playerAttack.IsAttacking; } } 
    public  bool IFrames { get { return iFrames; } } 
    public int Hp { get { return hp; } set { hp = value; } }

    private void Start()
    {
        playerAttack.SetValues(playerData.attackDamage, playerData.attackDuration,
                               playerData.attackCooldown, playerData.attackAreaSize, exits);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = inputHandler.Direction;

        float horizontal = direction.x;
        float vertical = direction.y;

        Vector3 velocity = new Vector3(horizontal, 0, vertical).normalized;

        if (velocity != Vector3.zero)
        {
            playerAttack.transform.rotation = Quaternion.LookRotation(velocity);
        }
        controller.Move(velocity * (Time.deltaTime * (playerData.speed * currentSpeedMultiplier)));
    }

    public void OnDash()
    {
        if (!playerData.canDashDuringAttack && playerAttack.IsAttacking)
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
        playerAttack.Attack();
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
        playerAttack.RemoveDeadEnemy(enemy);
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
