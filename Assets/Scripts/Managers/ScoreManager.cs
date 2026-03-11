using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private const string HIGH_SCORE_KEY = "HighScore";
    public int currentScore = 0;
    public int highScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }

    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void AddScore(int points)
    {
        currentScore += points;
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
            PlayerPrefs.Save();
        }
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }
}