using UnityEngine;
using UnityEngine.Events;
public class EnemySpawner : MonoBehaviour
{
    public Transform enemySpawnPos;
    public GameObject enemyPrefab;
    public UnityEvent onAllEnemiesDefeated;
    int enemiesAlive;

    public void SummonEnemies()
    {
        int enemyAmnt = Random.Range(1, 10);

        Debug.Log("Summoned " + enemyAmnt + " enemies");

        enemiesAlive = enemyAmnt;

        for (int i = 0; i < enemyAmnt; i++)
        {
            Vector2 randomOffset = new Vector2(
                Random.Range(-3f, 3f),
                Random.Range(-3f, 3f)
            );

            Vector2 spawnPos = (Vector2)enemySpawnPos.position + randomOffset;

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if(enemyScript != null)
            {
                enemyScript.OnDeath += HandleEnemyDeath;
            }
        }
    }

    void HandleEnemyDeath()
    {
        enemiesAlive--;

        Debug.Log("Enemies left: " + enemiesAlive);

        if (enemiesAlive <= 0)
        {
            Debug.Log("All enemies defeated!");

            onAllEnemiesDefeated?.Invoke();
        }
    }
}
