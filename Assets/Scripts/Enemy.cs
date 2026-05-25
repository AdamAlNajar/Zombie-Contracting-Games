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

        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceBetween)
        {
            rb.MovePosition(Vector2.MoveTowards(
                rb.position,
                player.transform.position,
                speed * Time.fixedDeltaTime
            ));

            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
}