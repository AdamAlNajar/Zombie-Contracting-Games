using UnityEngine;

public class GameData : MonoBehaviour
{
    public int gameCoins;
    public static GameData Instance;
    public bool prologFinished;
    public bool level1SeriesOfEventsCompleted;

    /// <summary>
    /// Tracks which missions (0–15) the player has completed.
    /// </summary>
    [SerializeField] private bool[] missionsCompleted;

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        // Initialize missions array to match total mission count
        missionsCompleted = new bool[MissionsDatabase.MissionCount];

        SaveSystem.LoadGame();
    }

    /// <summary>
    /// Check if a specific mission has been completed.
    /// </summary>
    public bool IsMissionCompleted(int index)
    {
        if (index < 0 || index >= missionsCompleted.Length)
            return false;
        return missionsCompleted[index];
    }

    /// <summary>
    /// Mark a mission as completed.
    /// </summary>
    public void SetMissionCompleted(int index)
    {
        if (index >= 0 && index < missionsCompleted.Length)
            missionsCompleted[index] = true;
    }

    /// <summary>
    /// Get the full completion status array for saving.
    /// </summary>
    public bool[] GetMissionsCompleted()
    {
        return missionsCompleted;
    }

    /// <summary>
    /// Restore the completion status array from saved data.
    /// </summary>
    public void SetMissionsCompleted(bool[] saved)
    {
        if (saved != null && saved.Length == MissionsDatabase.MissionCount)
            missionsCompleted = saved;
        // If lengths don't match, keep the existing initialized array
    }
}