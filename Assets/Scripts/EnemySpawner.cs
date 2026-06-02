using UnityEngine;
using UnityEngine.Events;
public class EnemySpawner : MonoBehaviour
{
    public Transform enemySpawnPos;
    public GameObject enemyPrefab;
    public UnityEvent onAllEnemiesDefeated;
    public MessageSystem messageSystem;

    int enemiesAlive;
    public int battleIndex = 0;

    public void SummonEnemies()
    {
        int enemyAmnt;

        switch (battleIndex)
        {
            case 0:
                enemyAmnt = Random.Range(10, 15);
                break;
            default:
                enemyAmnt = 2;
                break;
        }

        messageSystem.ShowMessage(
            enemyAmnt.ToString() + " enemies spawned, Kill them all...",
            5f
        );

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
            if (enemyScript != null)
            {
                enemyScript.OnDeath += HandleEnemyDeath;
            }
        }
    }

    void HandleEnemyDeath()
    {
        enemiesAlive--;

        Debug.Log("Enemies left: " + enemiesAlive);
        messageSystem.ShowMessage("Enemies left: " + enemiesAlive, 5f);

        if (enemiesAlive <= 0)
        {
            Debug.Log("All enemies defeated!");

            // ✅ ONLY CHANGE: replace PlayerPrefs with GameData
            if (battleIndex == 0)
            {
                GameData.Instance.prologFinished = true;
                onAllEnemiesDefeated?.Invoke();
            }

            onAllEnemiesDefeated?.Invoke();
        }
    }
}
