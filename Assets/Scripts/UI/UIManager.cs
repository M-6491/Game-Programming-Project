using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("HUD")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject levelCompletePanel;
    public GameObject gameCompletePanel;

    [Header("Buttons")]
    public Button resumeButton;
    public Button retryButton;
    public Button nextLevelButton;
    public Button pauseMenuButton;
    public Button gameOverMenuButton;
    public Button levelCompleteMenuButton;
    public Button gameCompleteMenuButton;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        HideAllPanels();
    }

    void Start()
    {
        UpdateScore(0);
        UpdateLives(3);

        if (resumeButton != null)
            resumeButton.onClick.AddListener(OnResumeClicked);
        if (retryButton != null)
            retryButton.onClick.AddListener(OnRetryClicked);
        if (nextLevelButton != null)
            nextLevelButton.onClick.AddListener(OnNextLevelClicked);
        if (pauseMenuButton != null)
            pauseMenuButton.onClick.AddListener(OnMenuClicked);
        if (gameOverMenuButton != null)
            gameOverMenuButton.onClick.AddListener(OnMenuClicked);
        if (levelCompleteMenuButton != null)
            levelCompleteMenuButton.onClick.AddListener(OnMenuClicked);
        if (gameCompleteMenuButton != null)
            gameCompleteMenuButton.onClick.AddListener(OnMenuClicked);
    }

    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    void OnResumeClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.ResumeGame();
    }

    void OnRetryClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.RestartLevel();
    }

    void OnNextLevelClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.LoadNextLevel();
    }

    void OnMenuClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.LoadMainMenu();
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = "DATA CLEANED: " + score + "KB";
    }

    public void UpdateLives(int lives)
    {
        if (livesText != null)
            livesText.text = "INTEGRITY: " + lives;
    }

    public void HideAllPanels()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (levelCompletePanel != null) levelCompletePanel.SetActive(false);
        if (gameCompletePanel != null) gameCompletePanel.SetActive(false);
    }

    public void ShowPause()
    {
        HideAllPanels();
        if (pausePanel != null) pausePanel.SetActive(true);
    }

    public void ShowGameOver()
    {
        HideAllPanels();
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    public void ShowLevelComplete()
    {
        HideAllPanels();
        if (levelCompletePanel != null) levelCompletePanel.SetActive(true);
    }

    public void ShowGameComplete()
    {
        HideAllPanels();
        if (gameCompletePanel != null) gameCompletePanel.SetActive(true);
    }
}