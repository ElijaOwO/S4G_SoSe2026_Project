using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExitScript : MonoBehaviour
{
   [SerializeField] private List<Enemy> enemies = new List<Enemy>();
   [SerializeField] private List<ItemInteractable> interactable = new List<ItemInteractable>();
   [SerializeField] private GameObject ExitBlocker;
   [SerializeField] private String scene;

   void OpenExit()
   {
      ExitBlocker.SetActive(false);

        foreach (ItemInteractable item in interactable)
        {
            item.SpawnPowerup();
        }
   }

   private void OnTriggerEnter(Collider other)
   {
      SceneManager.LoadScene(scene);
   }

   public void RemoveEnemy(Enemy enemy)
   {
      if (enemies.Contains(enemy))
      {
         enemies.Remove(enemy);
      }
      
      if (enemies.Count == 0)
      {
         OpenExit();
      }
   }
}
