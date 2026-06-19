using UnityEngine;

[CreateAssetMenu(fileName = "RangedEnemyData", menuName = "Scriptable Objects/RangedEnemyData")]
public class RangedEnemyData : EnemyData
{
    
    public float RepositionDistance;
    public float repositionCooldown;
    public float repositionTime;
}
