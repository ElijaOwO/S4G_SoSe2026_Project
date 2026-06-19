using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    public CharacterController controller;

    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private GameObject playerSpriteObject;

    private float currentSpeedMultiplier = 1.0f;

    private bool isDashing = false;
    private bool canDash = true;

    public static bool isAttacking = false;
    private bool canAttack = true;

    public static bool iFrames = false;


    void Update()
    {
        Vector2 direction = inputHandler.Direction;

        float horizontal = direction.x;
        float vertical = direction.y;

        Vector3 velocity = new Vector3(horizontal, 0, vertical).normalized;
        controller.Move(velocity * Time.deltaTime * (playerData.speed * currentSpeedMultiplier));
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
        if(!playerData.canAttackDuringDash && isDashing)
        {
            return;
        }
        else if(!isAttacking && canAttack)
        {
            StartCoroutine(Attack());
            StartCoroutine(StartAttackCooldown());
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

    private IEnumerator Attack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(playerData.attackDuration);
        isAttacking = false;
    }

    private IEnumerator StartAttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(playerData.attackCooldown);
        canAttack = true;
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
