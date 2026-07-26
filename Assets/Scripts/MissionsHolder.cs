using UnityEngine;
using TMPro;

public class MissionsHolder : MonoBehaviour
{
    public GameObject[] missions;
    private MissionsManager missionsManager;
    private Camera mainCamera;
    private Canvas labelCanvas;

    private void Awake()
    {
        missionsManager = FindFirstObjectByType<MissionsManager>();
        mainCamera = Camera.main;

        // Create or find a dedicated Canvas for mission labels
        // This ensures TextMeshProUGUI renders properly in 2D mode
        labelCanvas = FindFirstObjectByType<Canvas>();
        if (labelCanvas == null)
        {
            GameObject canvasObj = new GameObject("MissionLabelsCanvas");
            labelCanvas = canvasObj.AddComponent<Canvas>();
            labelCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            labelCanvas.sortingOrder = 100;
            canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
            canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
        }
    }

    public void DisplayMissions()
    {
        for (int i = 0; i < missions.Length; i++)
        {
            GameObject missionObj = missions[i];
            if (missionObj == null) continue;

            // Completed missions get removed from the hub entirely
            if (GameData.Instance != null && GameData.Instance.IsMissionCompleted(i))
            {
                Destroy(missionObj);
                continue;
            }

            missionObj.SetActive(true);

            AutoWireButton(missionObj, i);
            AddMissionLabel(missionObj, i);
        }
    }

    private void AutoWireButton(GameObject missionObj, int missionIndex)
    {
        ButtonScriptable btn = missionObj.GetComponent<ButtonScriptable>();
        if (btn == null)
            btn = missionObj.AddComponent<ButtonScriptable>();

        btn.onPressed.RemoveAllListeners();

        int capturedIndex = missionIndex;
        btn.onPressed.AddListener(() =>
        {
            if (missionsManager != null)
                missionsManager.LoadMission(capturedIndex);
        });
    }

    /// <summary>
    /// Creates Canvas-based TextMeshProUGUI labels that follow the button's world position.
    /// Uses FollowWorldTarget to track the button every frame in screen space.
    /// </summary>
    private void AddMissionLabel(GameObject missionObj, int missionIndex)
    {
        MissionDefinition data = MissionsDatabase.GetMission(missionIndex);
        string uid = missionIndex.ToString();

        // Check if already created (on the canvas)
        Transform existing = labelCanvas.transform.Find("MissionLabelGroup" + uid);
        if (existing != null)
            return;

        // Container on the Canvas — FollowWorldTarget keeps it above the button
        GameObject container = new GameObject("MissionLabelGroup" + uid);
        container.transform.SetParent(labelCanvas.transform, false);

        FollowWorldTarget follower = container.AddComponent<FollowWorldTarget>();
        follower.target = missionObj.transform;
        follower.mainCamera = mainCamera;
        follower.screenOffset = new Vector2(0f, 100f);

        // --- Mission title ---
        GameObject titleObj = new GameObject("MissionLabel" + uid);
        titleObj.transform.SetParent(container.transform, false);

        TextMeshProUGUI titleTmp = titleObj.AddComponent<TextMeshProUGUI>();
        titleTmp.text = $"Mission {missionIndex + 1}: {data.MissionName}";
        titleTmp.fontSize = 22;
        titleTmp.fontStyle = FontStyles.Bold;
        titleTmp.alignment = TextAlignmentOptions.Center;
        titleTmp.color = Color.white;

        RectTransform titleRt = titleTmp.rectTransform;
        titleRt.anchorMin = new Vector2(0.5f, 1f);
        titleRt.anchorMax = new Vector2(0.5f, 1f);
        titleRt.pivot = new Vector2(0.5f, 1f);
        titleRt.anchoredPosition = Vector2.zero;
        titleRt.sizeDelta = new Vector2(420, 30);

        // --- Subtitle ---
        GameObject subObj = new GameObject("SubtitleLabel" + uid);
        subObj.transform.SetParent(container.transform, false);

        TextMeshProUGUI subTmp = subObj.AddComponent<TextMeshProUGUI>();
        subTmp.text = data.Subtitle;
        subTmp.fontSize = 16;
        subTmp.fontStyle = FontStyles.Italic;
        subTmp.alignment = TextAlignmentOptions.Center;
        subTmp.color = new Color(0.85f, 0.85f, 0.85f);

        RectTransform subRt = subTmp.rectTransform;
        subRt.anchorMin = new Vector2(0.5f, 1f);
        subRt.anchorMax = new Vector2(0.5f, 1f);
        subRt.pivot = new Vector2(0.5f, 1f);
        subRt.anchoredPosition = new Vector2(0f, -32f);
        subRt.sizeDelta = new Vector2(420, 24);

        // --- Reward ---
        GameObject rewardObj = new GameObject("RewardLabel" + uid);
        rewardObj.transform.SetParent(container.transform, false);

        TextMeshProUGUI rewardTmp = rewardObj.AddComponent<TextMeshProUGUI>();
        rewardTmp.text = $"Reward: {data.CoinReward} coins  |  Enemies: {data.MinEnemies}-{data.MaxEnemies}";
        rewardTmp.fontSize = 14;
        rewardTmp.alignment = TextAlignmentOptions.Center;
        rewardTmp.color = new Color(1f, 0.85f, 0f); // Gold

        RectTransform rewardRt = rewardTmp.rectTransform;
        rewardRt.anchorMin = new Vector2(0.5f, 1f);
        rewardRt.anchorMax = new Vector2(0.5f, 1f);
        rewardRt.pivot = new Vector2(0.5f, 1f);
        rewardRt.anchoredPosition = new Vector2(0f, -58f);
        rewardRt.sizeDelta = new Vector2(420, 20);

        // Snap to initial position
        if (mainCamera != null)
            follower.Snap();
    }
}

/// <summary>
/// Follows a world-space Transform and positions this UI element at its screen position.
/// Attach to a Canvas child with a RectTransform.
/// </summary>
public class FollowWorldTarget : MonoBehaviour
{
    public Transform target;
    public Camera mainCamera;
    public Vector2 screenOffset;

    private RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        if (rt == null)
            rt = gameObject.AddComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if (target == null || mainCamera == null || rt == null)
            return;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);
        rt.position = new Vector3(screenPos.x + screenOffset.x, screenPos.y + screenOffset.y, screenPos.z);
    }

    /// <summary>
    /// Force an immediate snap to the target position (used on creation).
    /// </summary>
    public void Snap()
    {
        if (target == null || mainCamera == null || rt == null)
            return;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);
        rt.position = new Vector3(screenPos.x + screenOffset.x, screenPos.y + screenOffset.y, screenPos.z);
    }
}
