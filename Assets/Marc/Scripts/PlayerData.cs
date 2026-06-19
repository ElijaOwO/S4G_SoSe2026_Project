using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    public float speed = 5.0f;

    [Header("Dash Settings")]
    public bool toggleInstantSnapDash = false;
    public float snapDashDistance = 1.5f;
    public float dashMultiplier = 3.0f;
    public float dashDuration = 0.2f;
    public float dashDelay = 0.0f;
    public float dashCooldown = 2.0f;

    [Header("Attack Settings")]
    public float attackDuration = 0.1f;
    public float attackCooldown = 1.0f;

    [Header("I-Frames")]
    public bool toggleIFrames = false;
    public float iFramesDuration = 3.0f;
    public float iFramesBlinkSpeed = 0.3f;

    [Header("Restrictions")]
    public bool ignoreDamageDuringDash = false;
    public bool canAttackDuringDash = false;
    public bool canDashDuringAttack = false;
}
