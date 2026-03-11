using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PatrolBug") || other.CompareTag("ChaserVirus"))
        {
            player.TakeDamage();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PatrolBug") || other.CompareTag("ChaserVirus"))
        {
            player.TakeDamage();
        }
    }
}