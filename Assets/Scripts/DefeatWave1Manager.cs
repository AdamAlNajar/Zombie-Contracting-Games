using UnityEngine;

/// <summary>
/// This script is kept for backward compatibility.
/// When a wave is defeated, it signals the MissionController
/// which handles the completion flow (dialog → coins → return to hub).
/// 
/// If no MissionController is active, it falls back to the legacy behavior
/// of showing the mission board directly.
/// </summary>
public class DefeatWave1Manager : MonoBehaviour
{
    public Dialog dialog;
    private bool used = false;

    public void OnDefeat()
    {
        if (used) return;
        used = true;

        // Check if a MissionController is running this mission
        MissionController controller = FindFirstObjectByType<MissionController>();
        if (controller != null)
        {
            // MissionController will handle dialog and completion
            // We just need to set level1SeriesOfEventsCompleted for legacy compatibility
            GameData.Instance.level1SeriesOfEventsCompleted = true;
            SaveSystem.SaveGame();
            return;
        }

        // Legacy behavior (no MissionController found)
        dialog.dialogLines = new string[]
        {
            "??? : Impressive work, soldier. Didn't think you'd make it through that.",
            "You : Who are you? Show yourself.",
            "Contractor : Name's the Contractor. I'm the one who posted this job — and the one who signs your paychecks.",
            "Contractor : Every zombie you put down earns you coin. Every wave you clear, a bonus. Simple math.",
            "Contractor : The infection is spreading. The more you clear, the more we can push back.",
            "Contractor : Welcome to Zombie Contracting. It's a dirty job, but someone's gotta do it.",
            "Contractor : Your mission — clear every zone, collect every bounty, and take back this world one street at a time.",
            "Contractor : Use your earnings at the contract board to unlock new gear and upgrades. Stay sharp out there.",
            "Contractor : Check the mission terminal for available contracts. They're color-coded by difficulty.",
            "Contractor : That's enough talk. You've got work to do. Dismissed."
        };
        dialog.textBox.SetActive(true);
        dialog.StartDialog();
        GameData.Instance.level1SeriesOfEventsCompleted = true;
        SaveSystem.SaveGame();

        // Show missions immediately — no restart needed
        MissionsManager mm = FindFirstObjectByType<MissionsManager>();
        if (mm != null)
            mm.ShowMissions();

        Debug.Log("Saved Status");
    }
}
