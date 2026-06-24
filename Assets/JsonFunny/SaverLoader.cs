// Author: JET / MS / youtube
using System.IO;
using UnityEngine;

public class SaverLoader : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
[SerializeField] bool resetJson;


private void OnEnable()
{
    if (resetJson)
    {
        if (File.Exists(Application.dataPath + Path.AltDirectorySeparatorChar + "/SaveData.json"))
        {
           File.Delete(Application.dataPath + Path.AltDirectorySeparatorChar + "/SaveData.json");
        }
    }
}

private void Start()
{
    LoadData();
}

// Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SaveData()
    {
        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);

        using (StreamWriter witer =
               new StreamWriter(Application.dataPath + Path.AltDirectorySeparatorChar + "/SaveData.json"))
        {
            witer.Write(json);
        }
    }

    // Update is called once per frame
    public void LoadData()
    {
        if (!File.Exists(Application.dataPath + Path.AltDirectorySeparatorChar + "/SaveData.json"))
        {
            SaveData();
        }
        
       string json = string.Empty;

       using (StreamReader reader =
              new StreamReader(Application.dataPath + Path.AltDirectorySeparatorChar + "/SaveData.json"))
       {
           json = reader.ReadToEnd();
       }
      //int data = JsonUtility.FromJson<int>(json);
      //player.Hp = data;
    }
}
