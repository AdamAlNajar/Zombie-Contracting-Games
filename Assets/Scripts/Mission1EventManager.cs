using UnityEngine;

public class Mission1EventManager : MonoBehaviour
{
    public Dialog dialog;
    public EnemySpawner eSpawner;
    public void OnMissionCompleted() {
        GameData.Instance.gameCoins += Random.Range(10, 51);
    }

    public void Start()
    {
        dialog.dialogLines = new string[]
        {
                "Welcome to your first mission, soldier.",
                "This is Lettol City — once the crown jewel of Grore. Now it's a graveyard.",
                "The outbreak started three days ago. 80% of the population is already lost.",
                "Your objective: clear the sector. Every zombie you put down buys the survivors more time.",
                "We've lost too much ground already. Don't let them take any more.",
                "Battle commencing. Make every shot count."
        };

        // IMPORTANT: Activate textBox BEFORE starting dialog
        dialog.textBox.SetActive(true);

        dialog.StartDialog();
    }

    public void Update()
    {
        if (!dialog.DialogActive)
        {
            eSpawner.SummonEnemies();
            // Deactivate this script after spawning to avoid repeated calls
            enabled = false;
        }
    }
}
