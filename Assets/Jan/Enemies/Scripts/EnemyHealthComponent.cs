using UnityEngine;

public class EnemyHealthComponent : MonoBehaviour
{
    [SerializeField] private EnemyUI ui; 
    [SerializeField] private int maxHp = 10;
    public int MaxHp { get => maxHp;}
    
    private void Start()
    {
        ui.SetHealthBarMaxHp(maxHp);
    }

    public void Updatehealth(int damage)
    {
        ui.UpdateHealthbar(damage);
    }
}
