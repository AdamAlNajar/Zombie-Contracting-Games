using UnityEngine;

public class MissionsHolder : MonoBehaviour
{
    public GameObject[] missions;

    public void DisplayMissions()
    {
        for (int i = 0; i < missions.Length; i++)
        {
            missions[i].SetActive(true);
        }
    }
}
