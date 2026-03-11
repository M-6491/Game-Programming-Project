using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.3f;
    public LayerMask groundLayer;

    [Header("Status")]
    public int lives = 3;
    public int score = 0;
    public float damageCooldown = 1f;

    [Header("Attack")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireCooldown = 0.4f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;
    private float lastDamageTime = -999f;
    private float lastFireTime = -999f;
    private float facingDirection = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lives = 3;
        score = 0;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateLives(lives);
            UIManager.Instance.UpdateScore(score);
        }
    }

    void Update()
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.currentState != GameManager.GameState.Playing)
            return;

        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0) facingDirection = 1f;
        else if (moveInput < 0) facingDirection = -1f;

        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position, groundCheckRadius, groundLayer);

        if ((Input.GetKeyDown(KeyCode.Space) ||
             Input.GetKeyDown(KeyCode.W) ||
             Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayJump();
        }

        if (Input.GetKeyDown(KeyCode.X))
            Shoot();

        if (transform.position.y < -10f)
            Die();

        CheckEnemyContact();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.currentState != GameManager.GameState.Playing)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void Shoot()
    {
        if (Time.time - lastFireTime < fireCooldown) return;
        if (projectilePrefab == null) return;

        lastFireTime = Time.time;

        Vector3 spawnPos = firePoint != null ?
            firePoint.position : transform.position;

        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        Projectile p = proj.GetComponent<Projectile>();
        if (p != null)
            p.SetDirection(facingDirection);
    }

    void CheckEnemyContact()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.6f);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("PatrolBug") || hit.CompareTag("ChaserVirus"))
            {
                TakeDamage();
                return;
            }
        }
    }

    public void TakeDamage()
    {
        if (Time.time - lastDamageTime < damageCooldown) return;
        if (lives <= 0) return;

        lastDamageTime = Time.time;
        lives--;
        lives = Mathf.Max(0, lives);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayDamage();
        if (UIManager.Instance != null)
            UIManager.Instance.UpdateLives(lives);

        if (lives <= 0) Die();
    }

    public void AddScore(int points)
    {
        score += points;
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayCollect();
        if (UIManager.Instance != null)
            UIManager.Instance.UpdateScore(score);
    }

    void Die()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.TriggerGameOver();
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.6f);
    }
}