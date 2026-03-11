using UnityEngine;
using TMPro;

public class Hazard : MonoBehaviour
{
    public float flashSpeed = 4f;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        CreateLabel();
    }

    void CreateLabel()
    {
        GameObject labelObj = new GameObject("HazardLabel");
        labelObj.transform.SetParent(transform);
        labelObj.transform.localPosition = new Vector3(0, 0.4f, 0);


        TextMeshPro tmp = labelObj.AddComponent<TextMeshPro>();
        tmp.text = "CORRUPTED SECTOR";
        tmp.fontSize = 1.5f;
        tmp.color = new Color(1f, 0.3f, 1f);
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontStyle = FontStyles.Bold;
    }

    void Update()
    {
        // Glitch pulsing effect
        float alpha = Mathf.Abs(Mathf.Sin(Time.time * flashSpeed));
        sr.color = new Color(0.7f, 0f, 1f, 0.4f + alpha * 0.6f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage();
        }
    }
}