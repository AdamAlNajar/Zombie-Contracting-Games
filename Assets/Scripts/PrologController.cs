using UnityEngine;

public class PrologController : MonoBehaviour
{
    public EnemySpawner spawner;

    private void Start()
    {
        spawner.SummonEnemies();
    }

    public void OnPrologueCompleted()
    {
        GameData.Instance.prologFinished = true;

        SceneFlowManager.Instance.LoadGame();
    }
}
