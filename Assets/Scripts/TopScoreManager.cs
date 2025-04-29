using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class TopScoreManager : MonoBehaviour
{
    public TextMeshProUGUI HighScoreList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayHighScores();
    }
    void DisplayHighScores()
    {
        List<SaveData> highScores = DataManager.Instance.GetHighScoreList();
        HighScoreList.text = "";

        for (int i = 0; i < highScores.Count; i++)
        {
            string playerName = highScores[i].playerName;
            int score = highScores[i].highScore;

            HighScoreList.text += $"{i + 1}. {playerName} - {score}\n";
        }

        if (highScores.Count == 0)
        {
            HighScoreList.text = "No high scores yet!";
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
}
