using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TextMeshProUGUI highScoreText;

    private void Start()
    {
        ShowTopHighScore();
    }
    void ShowTopHighScore()
    {
        var highScores = DataManager.Instance.GetHighScoreList();
        if(highScores != null&& highScores.Count > 0)
        {
            var top = highScores[0];
            highScoreText.text = $"Best Score : {top.playerName} : {top.highScore}";
        }
        else
        {
            highScoreText.text = "Best Score: None";
        }
    }
    public void StartNew()
    {
        string inputName = nameInputField.text;
        DataManager.Instance.playerName = string.IsNullOrEmpty(inputName) ? "Unknown" : inputName;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Debug.Log("세이브");
        DataManager.Instance.SaveHighScores();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else   
        Application.Quit();
#endif
    }
}
