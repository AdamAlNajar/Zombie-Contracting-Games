using UnityEngine;
using TMPro;

public class MissionsHolder : MonoBehaviour
{
    public GameObject[] missions;

    public void DisplayMissions()
    {
        for (int i = 0; i < missions.Length; i++)
        {
            GameObject missionObj = missions[i];
            if (missionObj == null) continue;

            missionObj.SetActive(true);

            // Add a text label above the mission button if one doesn't exist
            AddMissionLabel(missionObj, i + 1);
        }
    }

    /// <summary>
    /// Adds or updates a "Mission X" label as a child of the mission GameObject.
    /// Skips if a label with the same name already exists.
    /// </summary>
    private void AddMissionLabel(GameObject missionObj, int missionNumber)
    {
        string labelName = "MissionLabel";
        Transform existing = missionObj.transform.Find(labelName);
        if (existing != null)
            return; // Label already added

        // Create a label GameObject as a child
        GameObject labelObj = new GameObject(labelName);
        labelObj.transform.SetParent(missionObj.transform, false);

        TMP_Text tmp = labelObj.AddComponent<TextMeshProUGUI>();
        tmp.text = $"Mission {missionNumber}";
        tmp.fontSize = 24;
        tmp.fontStyle = FontStyles.Bold;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.white;

        // Position it above the button
        RectTransform rt = tmp.rectTransform;
        rt.anchorMin = new Vector2(0.5f, 1f);   // Top-center of parent
        rt.anchorMax = new Vector2(0.5f, 1f);
        rt.pivot = new Vector2(0.5f, 1f);
        rt.anchoredPosition = new Vector2(0f, 15f); // Clear visual separation
        rt.sizeDelta = new Vector2(240, 36);
    }
}
