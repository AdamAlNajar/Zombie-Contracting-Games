using UnityEngine;

/// <summary>
/// Manages the Mission 1 intro flow.
/// If mission data is available (SelectedMissionIndex >= 0), dynamically adds
/// a MissionController to handle the full flow (intro dialog → spawn → completion → hub).
/// Otherwise falls back to legacy hardcoded behavior.
/// </summary>
public class Mission1EventManager : MonoBehaviour
{
    public Dialog dialog;
    public EnemySpawner eSpawner;
    private bool delegated = false;

    public void OnMissionCompleted()
    {
        GameData.Instance.gameCoins += Random.Range(10, 51);
    }

    public void Awake()
    {
        // If we have mission data from MissionsManager, auto-add MissionController
        if (MissionsManager.SelectedMissionIndex >= 0 &&
            MissionsManager.SelectedMissionIndex < MissionsDatabase.MissionCount)
        {
            MissionController controller = GetComponent<MissionController>();
            if (controller == null)
            {
                controller = gameObject.AddComponent<MissionController>();
            }
            // The MissionController's Awake will read SelectedMissionIndex and set everything up
            delegated = true;
        }
    }

    public void Start()
    {
        // If MissionController was added in Awake, skip our legacy logic
        if (delegated)
            return;

        // Legacy: no mission data — run original intro dialog
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
        if (delegated)
            return;

        if (!dialog.DialogActive)
        {
            eSpawner.SummonEnemies();
            // Deactivate this script after spawning to avoid repeated calls
            enabled = false;
        }
    }
}
