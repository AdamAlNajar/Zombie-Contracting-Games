using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float currentHealth;
    public float startingHealth = 100f;

        private void Start()
    {
        currentHealth = startingHealth;

        if (HealthBar.Instance == null)
        {
            Debug.LogError("HealthBar missing in scene!");
            return;
        }

        HealthBar.Instance.SetMaxHealth(startingHealth);
    }



    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        HealthBar.Instance.SetHealth(currentHealth);

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
