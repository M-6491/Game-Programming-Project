using UnityEngine;

public class DataBit : MonoBehaviour
{
    [Header("Settings")]
    public int pointValue = 10;
    public float bobSpeed = 2f;
    public float bobHeight = 0.3f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Bobbing animation so it looks alive
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().AddScore(pointValue);
            Destroy(gameObject);
        }
    }
}