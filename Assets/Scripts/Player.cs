using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float currentHealth;
    public float startingHealth = 100f;
    public static Player instance;
    public HealthBar healthBar;

    private void Start()
    {
        currentHealth = startingHealth;
        healthBar.SetMaxHealth(startingHealth);
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
