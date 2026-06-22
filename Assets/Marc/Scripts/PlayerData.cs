// Author: MS
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("MOVEMENT")]
    public float speed = 5.0f;

    [Header("DASH SETTINGS")]
    public bool toggleInstantSnapDash = false;
    public float snapDashDistance = 1.5f;
    public float dashMultiplier = 3.0f;
    public float dashDuration = 0.2f;
    public float dashDelay = 0.0f;
    public float dashCooldown = 2.0f;

    [Header("ATTACK SETTINGS")]
    public int attackDamage = 1;
    public float attackDuration = 0.1f;
    public float attackCooldown = 1.0f;

    [Header("I-FRAMES")]
    public bool toggleIFrames = false;
    public float iFramesDuration = 3.0f;
    public float iFramesBlinkSpeed = 0.3f;

    [Header("RESTRICTIONS")]
    public bool ignoreDamageDuringDash = false;
    public bool canAttackDuringDash = false;
    public bool canDashDuringAttack = false;
}
