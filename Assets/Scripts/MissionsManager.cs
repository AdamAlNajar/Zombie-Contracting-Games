using UnityEngine;

public class MissionsManager : MonoBehaviour
{
    public MissionsHolder mHolder;

    /// <summary>
    /// Which mission index the player selected (set before loading the mission scene).
    /// Read by MissionController on scene start.
    /// </summary>
    public static int SelectedMissionIndex { get; set; } = -1;

    void Start()
    {
        // Auto-find MissionsHolder if not assigned in Inspector
        if (mHolder == null)
            mHolder = FindFirstObjectByType<MissionsHolder>();

        if (mHolder == null)
        {
            Debug.LogError("MissionsManager: No MissionsHolder found in the scene!");
            return;
        }

        if (GameData.Instance.level1SeriesOfEventsCompleted)
        {
            mHolder.DisplayMissions();
        }
    }

    /// <summary>
    /// Called externally (e.g. from DefeatWave1Manager) to show missions
    /// immediately after the intro cutscene, without requiring a scene restart.
    /// </summary>
    public void ShowMissions()
    {
        if (mHolder == null)
            mHolder = FindFirstObjectByType<MissionsHolder>();

        if (mHolder != null)
            mHolder.DisplayMissions();
        else
            Debug.LogError("MissionsManager: Cannot show missions — no MissionsHolder found!");
    }

    /// <summary>
    /// Load a mission by its index (0–15). Sets SelectedMissionIndex then loads the mission scene.
    /// Mission 1 = "Mission 1", Mission 2 = "Mission 2", etc.
    /// </summary>
    public void LoadMission(int missionIndex)
    {
        SelectedMissionIndex = missionIndex;
        string sceneName = $"Mission {missionIndex + 1}";
        SceneFlowManager.Instance.LoadMission(sceneName);
    }

    /// <summary>
    /// Legacy method — loads Mission 1 (index 0).
    /// </summary>
    public void LoadMission1()
    {
        LoadMission(0);
    }
}