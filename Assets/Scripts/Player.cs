using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float currentHealth;
    public float startingHealth = 100f;
    public HealthBar healthBar;

    private void Start()
    {
        currentHealth = startingHealth;
        healthBar.SetMaxHealth(startingHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

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
