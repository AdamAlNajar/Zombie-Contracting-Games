using UnityEngine;

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
}