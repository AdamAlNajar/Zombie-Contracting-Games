using UnityEngine;
using System;
public class Enemy : MonoBehaviour
{
   public float damage = 5f;
    public float startingHealth = 200f;
    float currentHealth;
    public float attackCooldown = 1f;
    float attackTimer;
    public Action OnDeath;
    public float speed = 3f;
    float distance;
    public float distanceBetween = 4f;
    public float separationRadius = 1f;
    public float separationStrength = 2f;


    Rigidbody2D rb;

    private void Start()
    {
        currentHealth = startingHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && attackTimer <= 0f)
        {
            other.gameObject.GetComponent<Player>().TakeDamage(damage);

            attackTimer = attackCooldown;
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && attackTimer <= 0f)
        {
            other.gameObject.GetComponent<Player>().TakeDamage(damage);

            attackTimer = attackCooldown;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;
    }

    public void FixedUpdate()
    {
        Player player = FindFirstObjectByType<Player>();
        if (player == null) return;

        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance >= distanceBetween)
            return;

        Vector2 toPlayer = (player.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;

        // --- SEPARATION (ENEMIES ONLY) ---
        Collider2D[] nearby = Physics2D.OverlapCircleAll(transform.position, separationRadius);

        Vector2 separation = Vector2.zero;

        foreach (var col in nearby)
        {
            if (col.gameObject == gameObject) continue;

            // IMPORTANT: ignore player completely
            if (col.CompareTag("Player")) continue;

            Vector2 diff = (Vector2)(transform.position - col.transform.position);
            float dist = diff.magnitude;

            if (dist > 0)
                separation += diff.normalized / dist;
        }

        // --- COMBINE MOVEMENT ---
        Vector2 moveDir = (toPlayer + separation * separationStrength).normalized;

        Vector2 target = rb.position + moveDir * speed * Time.fixedDeltaTime;

        rb.MovePosition(target);

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}