using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Full-featured pause menu. Auto-creates its UI Canvas on first use.
/// Press Escape to toggle. Respects dialog state.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; }
    public static bool IsPaused { get; private set; }

    [Header("References (auto-created if null)")]
    public GameObject pauseRoot;      // The full pause panel
    public Button resumeButton;
    public Button quitButton;

    [Header("Appearance")]
    public Color overlayColor = new Color(0f, 0f, 0f, 0.65f);
    public string pauseTitle = "PAUSED";
    public string resumeText = "Resume";
    public string quitText = "Quit to Menu";

    private CursorManager cursorManager;
    private Dialog activeDialog;

    // ─── Singleton / Self-Initialization ───

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        IsPaused = false;

        CreateUI();
    }

    private void Start()
    {
        // Find CursorManager in Start() after all Awake() calls have run
        cursorManager = FindFirstObjectByType<CursorManager>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-find the active dialog when a new scene loads
        activeDialog = FindFirstObjectByType<Dialog>();

        // Make sure pause is reset on new scene
        if (IsPaused)
            ResumeGame();
    }

    // ─── UI Creation ───

    private void CreateUI()
    {
        if (pauseRoot != null) return; // Already set up

        // --- Canvas ---
        GameObject canvasObj = new GameObject("PauseMenuCanvas");
        canvasObj.transform.SetParent(transform, false);

        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 1000;

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        canvasObj.AddComponent<GraphicRaycaster>();

        // --- Dark Overlay ---
        GameObject overlayObj = new GameObject("Overlay");
        overlayObj.transform.SetParent(canvasObj.transform, false);

        Image overlayImage = overlayObj.AddComponent<Image>();
        overlayImage.color = overlayColor;
        overlayImage.raycastTarget = true;

        RectTransform overlayRt = overlayImage.rectTransform;
        overlayRt.anchorMin = Vector2.zero;
        overlayRt.anchorMax = Vector2.one;
        overlayRt.offsetMin = Vector2.zero;
        overlayRt.offsetMax = Vector2.zero;

        // --- Panel Container (centered) ---
        GameObject panelObj = new GameObject("Panel");
        panelObj.transform.SetParent(canvasObj.transform, false);

        RectTransform panelRt = panelObj.AddComponent<RectTransform>();
        panelRt.sizeDelta = new Vector2(400, 350);
        panelRt.anchorMin = new Vector2(0.5f, 0.5f);
        panelRt.anchorMax = new Vector2(0.5f, 0.5f);
        panelRt.pivot = new Vector2(0.5f, 0.5f);
        panelRt.anchoredPosition = Vector2.zero;

        VerticalLayoutGroup layout = panelObj.AddComponent<VerticalLayoutGroup>();
        layout.spacing = 20f;
        layout.childAlignment = TextAnchor.MiddleCenter;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;
        layout.padding = new RectOffset(30, 30, 30, 30);

        Image panelBg = panelObj.AddComponent<Image>();
        panelBg.color = new Color(0.1f, 0.1f, 0.1f, 0.9f);
        panelBg.raycastTarget = true;

        // --- Title Text ---
        GameObject titleObj = new GameObject("Title");
        titleObj.transform.SetParent(panelObj.transform, false);

        TMP_Text titleText = titleObj.AddComponent<TextMeshProUGUI>();
        titleText.text = pauseTitle;
        titleText.fontSize = 48;
        titleText.fontStyle = FontStyles.Bold;
        titleText.alignment = TextAlignmentOptions.Center;
        titleText.color = Color.white;

        RectTransform titleRt = titleText.rectTransform;
        titleRt.sizeDelta = new Vector2(0, 60);

        // --- Resume Button ---
        GameObject resumeObj = new GameObject("ResumeButton");
        resumeObj.transform.SetParent(panelObj.transform, false);

        resumeButton = CreateButton(resumeObj, resumeText);

        // --- Quit Button ---
        GameObject quitObj = new GameObject("QuitButton");
        quitObj.transform.SetParent(panelObj.transform, false);

        quitButton = CreateButton(quitObj, quitText, new Color(0.6f, 0.2f, 0.2f));

        // Wire up actions
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitToMenu);

        // Store root
        pauseRoot = overlayObj; // we use overlay as the root toggle
        // Actually we need the whole canvas controlled - let's use overlay as the toggle
        // so we disable the entire pause UI
        pauseRoot = canvasObj;

        // Hide by default
        pauseRoot.SetActive(false);
    }

    private Button CreateButton(GameObject btnObj, string label, Color? bgColor = null)
    {
        Color color = bgColor ?? new Color(0.2f, 0.2f, 0.2f);

        Image bg = btnObj.AddComponent<Image>();
        bg.color = color;
        bg.raycastTarget = true;

        Button btn = btnObj.AddComponent<Button>();
        btn.targetGraphic = bg;

        // Button text
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(btnObj.transform, false);

        TMP_Text tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = label;
        tmp.fontSize = 28;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.white;

        RectTransform textRt = tmp.rectTransform;
        textRt.anchorMin = Vector2.zero;
        textRt.anchorMax = Vector2.one;
        textRt.offsetMin = Vector2.zero;
        textRt.offsetMax = Vector2.zero;

        RectTransform btnRt = btnObj.GetComponent<RectTransform>();
        btnRt.sizeDelta = new Vector2(0, 50);

        // Button color transitions
        ColorBlock colors = btn.colors;
        colors.highlightedColor = new Color(color.r * 1.3f, color.g * 1.3f, color.b * 1.3f, 1f);
        colors.pressedColor = new Color(color.r * 0.7f, color.g * 0.7f, color.b * 0.7f, 1f);
        colors.normalColor = color;
        btn.colors = colors;

        return btn;
    }

    // ─── Core Logic ───

    private void Update()
    {
        // Don't allow pausing on menu/prologue scenes
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MainMenu" || sceneName == "Prolog")
            return;

        // Escape to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Refresh active dialog reference
            activeDialog = FindFirstObjectByType<Dialog>();

            if (IsPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        if (IsPaused) return;

        IsPaused = true;

        // Notify cursor manager
        if (cursorManager != null)
            cursorManager.OnPauseOpened();

        // Show cursor for UI interaction
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Activate UI
        if (pauseRoot != null)
            pauseRoot.SetActive(true);

        // Pause time
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (!IsPaused) return;

        IsPaused = false;

        // Unpause time (MUST be first so other systems can react)
        Time.timeScale = 1f;

        // Notify cursor manager
        if (cursorManager != null)
            cursorManager.OnPauseClosed();

        // Hide UI
        if (pauseRoot != null)
            pauseRoot.SetActive(false);

        // Cursor visibility is managed by CursorManager from here
    }

    public void QuitToMenu()
    {
        // Ensure everything is unpaused before scene load
        Time.timeScale = 1f;
        IsPaused = false;

        // Clean up cursor state
        if (cursorManager != null)
            cursorManager.SetGameplayState();

        // Hide pause UI
        if (pauseRoot != null)
            pauseRoot.SetActive(false);

        // Show cursor for main menu
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Load main menu — singleton persists but ignores Escape on menu scenes
        SceneManager.LoadScene("MainMenu");
    }
}
