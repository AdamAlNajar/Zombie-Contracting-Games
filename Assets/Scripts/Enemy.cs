using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 5f;
    public float startingHealth = 200f;
    float currentHealth;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
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
        Destroy(gameObject);
    }
}