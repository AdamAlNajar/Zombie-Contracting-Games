using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float currentHealth;
    public float startingHealth = 100f;
    public static Player instance;

    private void Start()
    {
        currentHealth = startingHealth;
        
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
