using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Object scene;
    [SerializeField] private StartMenu startMenu;
    [SerializeField] private FileMenu fileMenu;

    public void LoadScene()
    {
        if (scene != null)
        {
            SceneManager.LoadScene(scene.name);
        }
        else
        {
            Debug.Log("Scene was not assigned in Serialize Field.");
        }
    }

    public void OnExitButtonClicked()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    public void ShowStartMenu()
    {
        startMenu.gameObject.SetActive(true);
        fileMenu.gameObject.SetActive(false);
    }

    public void HideStartMenu()
    {
        startMenu.gameObject.SetActive(false);
        fileMenu.gameObject.SetActive(true);
    }
}