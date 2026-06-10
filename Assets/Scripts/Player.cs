using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
    [SerializeField] float currentHealth;
    [SerializeField] TMP_Text coinsText;
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
        if(coinsText != null)
            coinsText.text = GameData.Instance.gameCoins.ToString();
    }

    void Update()
    {
        if(coinsText != null)
        {
            coinsText.text = GameData.Instance.gameCoins.ToString();
            coinsText.color = Color.white;
        }
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
