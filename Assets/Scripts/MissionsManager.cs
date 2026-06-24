using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionsManager : MonoBehaviour
{
    public MissionsHolder mHolder;

    void Start()
    {
        if(GameData.Instance.level1SeriesOfEventsCompleted == true)
        {
            mHolder.DisplayMissions();
        }
    }

    public void LoadMission1()
    {
        SceneManager.LoadScene("Mission 1");
    }
}