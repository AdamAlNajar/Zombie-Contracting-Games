using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float currentHealth;
    public float startingHealth = 100f;

    private void Start()
    {
        currentHealth = startingHealth;
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
