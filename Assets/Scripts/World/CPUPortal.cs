using UnityEngine;
using TMPro;

public class CPUPortal : MonoBehaviour
{
    public float rotateSpeed = 90f;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // Add label above portal
        GameObject labelObj = new GameObject("PortalLabel");
        labelObj.transform.SetParent(transform);
        labelObj.transform.localPosition = new Vector3(0, 1.2f, 0);

        TextMeshPro tmp = labelObj.AddComponent<TextMeshPro>();
        tmp.text = "[ CPU CORE ]";
        tmp.fontSize = 2f;
        tmp.color = new Color(0f, 1f, 0.7f);
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontStyle = FontStyles.Bold;
    }

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        // Pulsing glow
        float pulse = Mathf.Abs(Mathf.Sin(Time.time * 2f));
        if (sr != null)
            sr.color = new Color(0f, 0.8f + pulse * 0.2f, 0.6f + pulse * 0.4f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.TriggerLevelComplete();
        }
    }
}