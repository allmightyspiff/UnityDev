using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public Color TeamColor;

    private void Awake()
    {
        // If there is already an instance, we just kill this new instance in favor of the old one.
        if (Instance != null) {
            
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Instance.TeamColor = Color.black;
        DontDestroyOnLoad(gameObject);
        LoadColor();
    }

    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        SaveData data = new SaveData{TeamColor = TeamColor};
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/saveFile.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            TeamColor = data.TeamColor;
        } else {
            TeamColor = Color.black;
        }
    }

}

