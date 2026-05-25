using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemySpawnPos;
    public GameObject enemyPrefab;

    public void SummonEnemies()
    {
        int enemyAmnt = Random.Range(1, 10);

        Debug.Log("Summoned " + enemyAmnt + " enemies");

        for (int i = 0; i < enemyAmnt; i++)
        {
            Vector2 randomOffset = new Vector2(
                Random.Range(-3f, 3f),
                Random.Range(-3f, 3f)
            );

            Vector2 spawnPos = (Vector2)enemySpawnPos.position + randomOffset;

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
        // make a function to display splash and take to main enu after killing all
    }
}
