using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        GameOver,
        LevelComplete,
        GameComplete,
        Briefing
    }

    public GameState currentState;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        MissionBriefing briefing = FindFirstObjectByType<MissionBriefing>();
        if (briefing == null)
            ChangeState(GameState.Playing);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (currentState == GameState.Playing)
                PauseGame();
            else if (currentState == GameState.Paused)
                ResumeGame();
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                if (UIManager.Instance != null)
                    UIManager.Instance.HideAllPanels();
                break;

            case GameState.Briefing:
                Time.timeScale = 0f;
                if (UIManager.Instance != null)
                    UIManager.Instance.HideAllPanels();
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                if (UIManager.Instance != null)
                    UIManager.Instance.ShowPause();
                break;

            case GameState.GameOver:
                Time.timeScale = 0f;
                if (UIManager.Instance != null)
                    UIManager.Instance.ShowGameOver();
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayGameOver();
                break;

            case GameState.LevelComplete:
                Time.timeScale = 0f;
                if (UIManager.Instance != null)
                    UIManager.Instance.ShowLevelComplete();
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayLevelComplete();
                break;

            case GameState.GameComplete:
                Time.timeScale = 0f;
                if (UIManager.Instance != null)
                    UIManager.Instance.ShowGameComplete();
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayLevelComplete();
                break;

            case GameState.Menu:
                Time.timeScale = 1f;
                break;
        }
    }

    public void PauseGame() { ChangeState(GameState.Paused); }
    public void ResumeGame() { ChangeState(GameState.Playing); }
    public void TriggerGameOver() { ChangeState(GameState.GameOver); }
    public void TriggerLevelComplete() { ChangeState(GameState.LevelComplete); }
    public void TriggerGameComplete() { ChangeState(GameState.GameComplete); }

    public void RestartLevel()
    {
        if (UIManager.Instance != null)
            UIManager.Instance.HideAllPanels();
        Instance = null;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        if (UIManager.Instance != null)
            UIManager.Instance.HideAllPanels();
        Instance = null;
        Time.timeScale = 1f;
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextIndex);
        else
            TriggerGameComplete();
    }

    public void LoadMainMenu()
    {
        Instance = null;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}