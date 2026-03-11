using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        // Show saved high score
        int best = PlayerPrefs.GetInt("HighScore", 0);
        if (highScoreText != null)
            highScoreText.text = "BEST: " + best + "KB";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            StartGame();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}