using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 5f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }
}