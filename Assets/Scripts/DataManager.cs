using System.Collections.Generic;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;

[System.Serializable]
class SaveData
{
    public string playerName;
    public int highScore;
}
[System.Serializable]
public class SaveDataList
{
    public List<SaveData> highScoreList = new List<SaveData>();
}
public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string playerName = "Unknown";
    public string highScorePlayerName;
    public int highScore;
    

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScores();
    }
    public void SaveHighScores()
    {
        SaveData data = new SaveData();
        data.playerName = highScorePlayerName;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScorePlayerName = data.playerName;
            highScore = data.highScore;
        }
    }
}
