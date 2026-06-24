// Author: MS
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private ItemInteractable item;

    public void OnAttackButtonClicked()
    {
        playerData.attackDamage += 1;
        Debug.Log(playerData.attackDamage);
        item.HideUpgradeUI();
    }

    public void OnHealthButtonClicked()
    {
        playerData.maxHp += 5;
        playerData.currentHp += 5;
        Debug.Log(playerData.maxHp);
        Debug.Log(playerData.currentHp);
        item.HideUpgradeUI();
    }

    public void OnSpeedButtonClicked()
    {
        playerData.speed *= 1.1f;
        Debug.Log(playerData.speed);
        item.HideUpgradeUI();
    }
}
