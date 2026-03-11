using UnityEngine;

public class UIButtons : MonoBehaviour
{
    public void OnResumeClicked()
    {
        GameManager.Instance.ResumeGame();
    }

    public void OnRetryClicked()
    {
        GameManager.Instance.RestartLevel();
    }

    public void OnNextLevelClicked()
    {
        GameManager.Instance.LoadNextLevel();
    }

    public void OnMainMenuClicked()
    {
        GameManager.Instance.LoadMainMenu();
    }
}