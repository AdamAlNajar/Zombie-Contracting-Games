using UnityEngine;

public class Level1Trigger : MonoBehaviour
{
   public Dialog dialog;
   public EnemySpawner spawner;

   public void StartBattle()
    {
        dialog.dialogLines = new string[]
        {
            "Welcome to your demise...",
            "Because you did so well before, well... youre going to see something.. harder now",
            "Battle Commencing.."
        };
        dialog.onDialogComplete.AddListener(SpawnEnemiesAfterDialog);
        dialog.textBox.SetActive(true);
        dialog.StartDialog();
    }

    void SpawnEnemiesAfterDialog()
    {
        spawner.SummonEnemies();
    }
}
