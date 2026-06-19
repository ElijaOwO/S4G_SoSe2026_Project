using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Attack")]
    public float attackDistance = 1;
    public float attackCooldown = 1;
    public int attackDamage = 1;
}
