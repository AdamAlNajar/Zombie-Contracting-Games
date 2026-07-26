using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the mission scene flow — reads mission data from MissionsDatabase,
/// sets up intro dialog, enemy spawning, and handles completion/return to hub.
/// Place this on the same GameObject as Dialog in the mission scene.
/// </summary>
public class MissionController : MonoBehaviour
{
    [Header("References (auto-found if left empty)")]
    [SerializeField] private Dialog dialog;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private MessageSystem messageSystem;

    /// <summary>
    /// Which mission index to load (set by MissionsManager before loading).
    /// </summary>
    public int MissionIndex { get; set; } = -1;

    private MissionDefinition missionData;
    private bool missionCompleted = false;

    private void Awake()
    {
        // Read the mission index set by MissionsManager before scene load
        if (MissionIndex < 0)
            MissionIndex = MissionsManager.SelectedMissionIndex;

        if (MissionIndex < 0 || MissionIndex >= MissionsDatabase.MissionCount)
        {
            Debug.LogError($"MissionController: Invalid mission index {MissionIndex}");
            MissionIndex = 0;
        }

        missionData = MissionsDatabase.GetMission(MissionIndex);

        // Auto-find references
        if (dialog == null)
            dialog = FindFirstObjectByType<Dialog>();
        if (enemySpawner == null)
            enemySpawner = FindFirstObjectByType<EnemySpawner>();
        if (messageSystem == null)
            messageSystem = FindFirstObjectByType<MessageSystem>();

        if (dialog == null)
        {
            Debug.LogError("MissionController: No Dialog found in scene!");
            return;
        }
        if (enemySpawner == null)
        {
            Debug.LogError("MissionController: No EnemySpawner found in scene!");
            return;
        }

        // Configure the enemy spawner for this mission
        enemySpawner.battleIndex = missionData.BattleIndex;

        // Show the mission name as a message
        if (messageSystem != null)
        {
            messageSystem.ShowMessage(
                $"Mission {MissionIndex + 1}: {missionData.MissionName}",
                4f
            );
        }
    }

    private void Start()
    {
        if (dialog == null)
        {
            Debug.LogError("MissionController.Start: Dialog is null - cannot start mission!");
            return;
        }
        if (enemySpawner == null)
        {
            Debug.LogError("MissionController.Start: EnemySpawner is null - cannot start mission!");
            return;
        }

        // Start the intro dialog
        dialog.dialogLines = missionData.IntroDialogLines;

        // Clear any previous listener and add ours
        dialog.onDialogComplete.RemoveListener(OnIntroDialogComplete);
        dialog.onDialogComplete.AddListener(OnIntroDialogComplete);

        // Activate text box and start
        dialog.textBox.SetActive(true);
        dialog.StartDialog();
    }

    private void OnIntroDialogComplete()
    {
        // Intro dialog finished — spawn enemies
        enemySpawner.SummonEnemies();

        // Listen for all enemies defeated
        enemySpawner.onAllEnemiesDefeated.RemoveListener(OnAllEnemiesDefeated);
        enemySpawner.onAllEnemiesDefeated.AddListener(OnAllEnemiesDefeated);
    }

    private void OnAllEnemiesDefeated()
    {
        if (missionCompleted)
            return;
        missionCompleted = true;

        // Show completion dialog
        dialog.dialogLines = missionData.CompletionDialogLines;
        dialog.onDialogComplete.RemoveListener(OnCompletionDialogComplete);
        dialog.onDialogComplete.AddListener(OnCompletionDialogComplete);

        dialog.textBox.SetActive(true);
        dialog.StartDialog();
    }

    private void OnCompletionDialogComplete()
    {
        // Award coins
        int reward = missionData.CoinReward;
        GameData.Instance.gameCoins += reward;

        // Mark as completed in save data
        GameData.Instance.SetMissionCompleted(MissionIndex);

        // For mission 0, also set the legacy flag so the hub mission board shows up
        if (MissionIndex == 0)
            GameData.Instance.level1SeriesOfEventsCompleted = true;

        // Show reward message
        if (messageSystem != null)
        {
            messageSystem.ShowMessage(
                $"Mission Complete! +{reward} coins!",
                3f
            );
        }

        SaveSystem.SaveGame();

        // Return to hub after a short delay
        StartCoroutine(ReturnToHub());
    }

    private IEnumerator ReturnToHub()
    {
        yield return new WaitForSeconds(2.5f);

        if (SceneFlowManager.Instance != null)
            SceneFlowManager.Instance.LoadGame();
    }
}
