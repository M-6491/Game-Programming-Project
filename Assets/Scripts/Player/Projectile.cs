using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 12f;
    public int damage = 1;
    public float lifetime = 3f;
    private float direction = 1f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(float dir)
    {
        direction = dir;
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PatrolBug") || other.CompareTag("ChaserVirus"))
        {
            EnemyHealth eh = other.GetComponent<EnemyHealth>();
            if (eh != null)
                eh.TakeDamage(damage);
            Destroy(gameObject);
        }

        // Destroy on platform contact
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            Destroy(gameObject);
    }
}