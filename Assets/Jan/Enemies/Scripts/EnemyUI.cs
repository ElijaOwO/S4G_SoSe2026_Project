using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

public class EnemyUI : MonoBehaviour
{
    //[SerializeField] 
    private Camera cam;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image attackIndicator;
    [SerializeField] private TextMeshProUGUI healthBarText;
    [SerializeField] private GameObject rotatingParts;
    
    
    private float attackCooldown;
    private float attackWindunp;
    
    private int maxHp;
    private int currentHp;

    private void Start()
    {
        cam = Camera.main;
    }

    public void SetAttackValues(float cooldown, float windup, float range)
    {
        attackCooldown  = cooldown;
        attackWindunp  =  windup;
        Vector3 scale = new Vector3(range, range, 1);
        
        attackIndicator.gameObject.transform.localScale = scale;
    }

    public void AttackIndicatorWindup()
    {
        Tween.UIFillAmount(attackIndicator, new TweenSettings<float>(1, new TweenSettings(attackWindunp)));
    }

    public void AttackIndicatorCoolDown()
    {
        Tween.UIFillAmount(attackIndicator, new TweenSettings<float>(0, new TweenSettings(attackCooldown)));
    }

    // Update is called once per frame
    void Update()
    {
        rotatingParts.transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
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
