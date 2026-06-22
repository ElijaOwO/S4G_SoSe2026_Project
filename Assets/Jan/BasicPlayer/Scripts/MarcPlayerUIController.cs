// Author: JET
using UnityEngine;
using UnityEngine.UIElements;

public class MarcPlayerUIController : MonoBehaviour
{
    [SerializeField] private PlayerPrototype player;
    [SerializeField] private UIDocument uiDocument;
    
    private ProgressBar progressBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progressBar = uiDocument.rootVisualElement.Q<ProgressBar>("ProgressBar");
        if (progressBar != null)
        {
            progressBar.style.color = Color.darkGreen;
            progressBar.highValue = player.Hp;
            progressBar.lowValue = 0;
            UpdateProgressBar(player.Hp);
        }
    }

    public void UpdateProgressBar(int hp)
    {
        progressBar.value = hp;
        if (hp < hp * 1 / 3)
        {
            progressBar.style.color = Color.darkRed;
        }
        else if(hp < hp * 2 / 3)
        {
            progressBar.style.color = Color.darkGoldenRod;
        }
    }
}
