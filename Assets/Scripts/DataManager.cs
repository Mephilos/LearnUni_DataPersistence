using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
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

    public SaveDataList saveDataList = new SaveDataList();

    private string savePath;
    

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Application.persistentDataPath + "/savefile.json";
        LoadHighScores();
    }
    public void AddNewScore(int score)
    {
        SaveData newEntry = new SaveData
        {
            playerName = string.IsNullOrEmpty(playerName) ? "Unknown" : playerName,
            highScore = score
        };

        saveDataList.highScoreList.Add(newEntry);
        saveDataList.highScoreList.Sort((a,b) => b.highScore.CompareTo(a.highScore));

        if(saveDataList.highScoreList.Count > 5)
        {
            saveDataList.highScoreList.RemoveRange(5,saveDataList.highScoreList.Count - 5);
        }
        SaveHighScores();
    }
    public void SaveHighScores()
    {
        try
        {
        string json = JsonUtility.ToJson(saveDataList, true);
        File.WriteAllText(savePath, json);
        }
        catch (System.Exception e)
        {
            Debug.LogError("[DataManager] Save failed: " + e.Message);
        }
        
    }
    public void LoadHighScores()
    {
        if(File.Exists(savePath))
        {
            try
            {
                string json = File.ReadAllText(savePath);
                saveDataList = JsonUtility.FromJson<SaveDataList>(json);
            }
            catch(System.Exception e)
            {
                Debug.LogError("[DataManager] Load failed: " + e.Message);
            }
        }
    }
    public List<SaveData> GetHighScoreList()
    {
        return saveDataList.highScoreList;
    }
}
