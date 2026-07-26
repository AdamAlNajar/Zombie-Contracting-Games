using UnityEngine;

public class MissionsManager : MonoBehaviour
{
    public MissionsHolder mHolder;

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

    public void LoadMission1()
    {
        SceneFlowManager.Instance.LoadMission("Mission 1");
    }
}