// Author: MS
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class ItemInteractable : MonoBehaviour
{
    [SerializeField] private UpgradeUI upgradeUI;


    public void ShowUpgradeUI()
    {
        Time.timeScale = 0;
        upgradeUI.gameObject.SetActive(true);
    }

    public void HideUpgradeUI()
    {
        Time.timeScale = 1;
        upgradeUI.gameObject.SetActive(false);
    }

    public void SpawnPowerup()
    {
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            ShowUpgradeUI();
        }
    }
}
