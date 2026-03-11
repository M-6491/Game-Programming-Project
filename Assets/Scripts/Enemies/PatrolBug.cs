using UnityEngine;

public class PatrolBug : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float moveSpeed = 2f;
    public float waypointA = -2f;
    public float waypointB = 2f;

    private int direction = 1;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        if (transform.position.x >= waypointB)
            direction = -1;
        else if (transform.position.x <= waypointA)
            direction = 1;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            new Vector3(waypointA, transform.position.y, 0),
            new Vector3(waypointB, transform.position.y, 0));
        Gizmos.DrawSphere(new Vector3(waypointA, transform.position.y, 0), 0.15f);
        Gizmos.DrawSphere(new Vector3(waypointB, transform.position.y, 0), 0.15f);
    }
}