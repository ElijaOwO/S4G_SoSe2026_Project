// Author: JET

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarcPlayerUIController : MonoBehaviour
{
    [SerializeField] private PlayerPrototype player;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthBarText;
    private int currentHp;
    private int maxHp;
    
    public void SetHealthBarMaxHp(int maxHp, int currentHp)
    {
        this.maxHp = maxHp;
        this.currentHp = currentHp;
        UpdateHealthbar(currentHp);
    }

    public void UpdateHealthbar(int hp)
    {
        currentHp = hp;
        float fillamount = (float)currentHp / (float)maxHp;
        healthBar.fillAmount = fillamount;
        UpdateText();
    }

    public void UpdateText()
    {
        healthBarText.text = currentHp + " / " + maxHp;
    }
}
