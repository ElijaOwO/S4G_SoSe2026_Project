using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    //[SerializeField] 
    private Camera cam;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthBarText;
    
    private int maxHp;
    private int currentHp;

    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }

    public void SetHealthBarMaxHp(int hp)
    {
        maxHp = hp;
        currentHp = maxHp;
        UpdateText();
    }

    public void UpdateHealthbar(int damage)
    {
        currentHp -= damage;
        float fillamount = (float)currentHp / (float)maxHp;
        healthBar.fillAmount = fillamount;
        UpdateText();
    }

    public void UpdateText()
    {
        healthBarText.text = currentHp + " / " + maxHp;
    }
}
