using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static GameObject[] persistentObjects = new GameObject[1];
    public int objectIndex;

    void Awake()
    {
        if (objectIndex < 0 || objectIndex >= persistentObjects.Length)
        {
            Debug.LogError($"ObjectIndex {objectIndex} is out of bounds!", this);
            return;
        }

        if (persistentObjects[objectIndex] == null)
        {
            persistentObjects[objectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (persistentObjects[objectIndex] != gameObject);
        {
            Destroy(gameObject);
        }
    }
}
