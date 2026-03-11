using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 2;
    private int currentHealth;

    private SpriteRenderer sr;
    private Color originalColor;
    private Canvas healthCanvas;
    private Image healthFill;

    void Start()
    {
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        CreateHealthBar();
    }

    void CreateHealthBar()
    {
        // Create a world space canvas above the enemy
        GameObject canvasObj = new GameObject("HealthCanvas");
        canvasObj.transform.SetParent(transform);
        canvasObj.transform.localPosition = new Vector3(0, 1.3f, 0);
        canvasObj.transform.localScale = new Vector3(0.01f, 0.01f, 1f);

        healthCanvas = canvasObj.AddComponent<Canvas>();
        healthCanvas.renderMode = RenderMode.WorldSpace;

        RectTransform canvasRect = canvasObj.GetComponent<RectTransform>();
        canvasRect.sizeDelta = new Vector2(100, 15);

        // Background
        GameObject bgObj = new GameObject("BG");
        bgObj.transform.SetParent(canvasObj.transform, false);

        Image bgImage = bgObj.AddComponent<Image>();
        bgImage.color = new Color(0.2f, 0.2f, 0.2f, 1f);

        RectTransform bgRect = bgObj.GetComponent<RectTransform>();
        bgRect.anchorMin = Vector2.zero;
        bgRect.anchorMax = Vector2.one;
        bgRect.offsetMin = Vector2.zero;
        bgRect.offsetMax = Vector2.zero;

        // Fill
        GameObject fillObj = new GameObject("Fill");
        fillObj.transform.SetParent(canvasObj.transform, false);

        healthFill = fillObj.AddComponent<Image>();
        healthFill.color = new Color(0.2f, 1f, 0.2f, 1f);
        healthFill.type = Image.Type.Filled;
        healthFill.fillMethod = Image.FillMethod.Horizontal;
        healthFill.fillAmount = 1f;

        RectTransform fillRect = fillObj.GetComponent<RectTransform>();
        fillRect.anchorMin = Vector2.zero;
        fillRect.anchorMax = Vector2.one;
        fillRect.offsetMin = Vector2.zero;
        fillRect.offsetMax = Vector2.zero;
    }

    void UpdateHealthBar()
    {
        if (healthFill == null) return;
        float ratio = (float)currentHealth / maxHealth;
        healthFill.fillAmount = ratio;
        healthFill.color = Color.Lerp(
            new Color(1f, 0.2f, 0.2f),
            new Color(0.2f, 1f, 0.2f),
            ratio);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        UpdateHealthBar();
        StartCoroutine(FlashOnHit());
        if (currentHealth <= 0)
            Die();
    }

    System.Collections.IEnumerator FlashOnHit()
    {
        sr.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sr.color = originalColor;
    }

    void Die()
    {
        Player player = FindFirstObjectByType<Player>();
        if (player != null)
            player.AddScore(50);

        ShowNeutralizedLabel();

        PatrolBug pb = GetComponent<PatrolBug>();
        if (pb != null) pb.enabled = false;

        ChaserVirus cv = GetComponent<ChaserVirus>();
        if (cv != null) cv.enabled = false;

        Destroy(gameObject, 0.8f);
    }

    void ShowNeutralizedLabel()
    {
        GameObject labelObj = new GameObject("NeutralizedLabel");
        labelObj.transform.position = transform.position + Vector3.up * 1.5f;

        TextMeshPro tmp = labelObj.AddComponent<TextMeshPro>();
        tmp.text = "[ NEUTRALIZED ]";
        tmp.fontSize = 2.5f;
        tmp.color = new Color(0f, 1f, 0.5f);
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontStyle = FontStyles.Bold;

        Destroy(labelObj, 1f);
    }
}