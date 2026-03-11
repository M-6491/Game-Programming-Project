using UnityEngine;
using TMPro;
using System.Collections;

public class MissionBriefing : MonoBehaviour
{
    [Header("UI References")]
    public GameObject briefingPanel;
    public TextMeshProUGUI briefingText;

    [Header("Settings")]
    [TextArea(4, 10)]
    public string missionText;
    public float typingSpeed = 0.03f;
    public float displayDuration = 3f;

    private bool briefingActive = true;

    void Start()
    {
        if (briefingPanel != null)
            briefingPanel.SetActive(true);
        if (briefingText != null)
            briefingText.text = "";

        StartCoroutine(ShowBriefing());
    }

    IEnumerator ShowBriefing()
    {
        // Wait one frame for GameManager to finish Start()
        yield return null;

        if (GameManager.Instance != null)
            GameManager.Instance.ChangeState(GameManager.GameState.Briefing);

        string displayed = "";
        foreach (char c in missionText)
        {
            if (!briefingActive) break;
            displayed += c;
            if (briefingText != null)
                briefingText.text = displayed;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        if (briefingActive)
            yield return new WaitForSecondsRealtime(displayDuration);

        DismissBriefing();
    }

    void DismissBriefing()
    {
        briefingActive = false;
        if (briefingPanel != null)
            briefingPanel.SetActive(false);
        if (GameManager.Instance != null)
            GameManager.Instance.ChangeState(GameManager.GameState.Playing);
        Destroy(gameObject);
    }

    void Update()
    {
        if (briefingActive && Input.GetKeyDown(KeyCode.Return))
            DismissBriefing();
    }
}