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
        if(!string.IsNullOrEmpty(DataManager.Instance.highScorePlayerName))
        {
            highScoreText.text = $"Best Score: {DataManager.Instance.highScorePlayerName} : {DataManager.Instance.highScore}";
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartNew()
    {
        DataManager.Instance.playerName = nameInputField.text;
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
