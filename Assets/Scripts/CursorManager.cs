using UnityEngine;

/// <summary>
/// Manages cursor visibility and lock state.
/// - Gameplay: cursor hidden (but still tracks for aiming)
/// - Dialog/Pause: cursor visible for UI interaction
/// </summary>
public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    [Header("Settings")]
    public bool hideCursorDuringGameplay = true;

    private int dialogLocks = 0;   // counter: active dialogs
    private int pauseLocks = 0;    // counter: pause menu active

    private void Awake()
    {
        // Auto-create singleton if none exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetGameplayState();
        }
    }

    /// <summary>
    /// Ensures a CursorManager exists in the scene — creates one if needed.
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void AutoCreate()
    {
        if (Instance != null) return;
        
        GameObject go = new GameObject("CursorManager");
        go.AddComponent<CursorManager>();
        // Awake will handle the rest
    }

    private void Update()
    {
        // If any system has requested cursor visibility, show it
        bool shouldShow = (dialogLocks > 0 || pauseLocks > 0);

        if (hideCursorDuringGameplay)
        {
            Cursor.visible = shouldShow;
            Cursor.lockState = shouldShow ? CursorLockMode.None : CursorLockMode.Confined;
        }
    }

    // --- Public API ---

    /// <summary> Call when a dialog starts. Increments lock. </summary>
    public void OnDialogOpened()
    {
        dialogLocks++;
    }

    /// <summary> Call when a dialog ends. Decrements lock. </summary>
    public void OnDialogClosed()
    {
        if (dialogLocks > 0)
            dialogLocks--;
    }

    /// <summary> Call when pause menu opens. </summary>
    public void OnPauseOpened()
    {
        pauseLocks++;
    }

    /// <summary> Call when pause menu closes. </summary>
    public void OnPauseClosed()
    {
        if (pauseLocks > 0)
            pauseLocks--;
    }

    /// <summary> Force cursor to gameplay state (hidden). </summary>
    public void SetGameplayState()
    {
        dialogLocks = 0;
        pauseLocks = 0;
    }

    /// <summary> Is the cursor currently required by any system? </summary>
    public bool IsCursorRequired()
    {
        return dialogLocks > 0 || pauseLocks > 0;
    }
}
