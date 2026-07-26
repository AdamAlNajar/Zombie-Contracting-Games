using UnityEngine;

public class Level1Trigger : MonoBehaviour
{
   public Dialog dialog;
   public EnemySpawner spawner;

   public void StartBattle()
    {
        dialog.dialogLines = new string[]
        {
            "Heh... you actually made it past the first wave. Cute.",
            "But that was just the warm-up. Let's see how you handle something with a little more... bite.",
            "They're hungrier this time. And so am I.",
            "Battle commencing. Try not to die too fast."
        };
        // Remove previous listener to prevent duplicate calls if triggered again
        dialog.onDialogComplete.RemoveListener(SpawnEnemiesAfterDialog);
        dialog.onDialogComplete.AddListener(SpawnEnemiesAfterDialog);
        dialog.textBox.SetActive(true);
        dialog.StartDialog();
    }

    void SpawnEnemiesAfterDialog()
    {
        spawner.SummonEnemies();
    }
}
