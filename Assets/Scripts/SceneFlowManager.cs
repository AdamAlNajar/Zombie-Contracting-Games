using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager : MonoBehaviour
{
    public static SceneFlowManager Instance;
    public float splashDuration;

    private const string MAIN_MENU_SCENE = "MainMenu";
    private const string PROLOGUE_SCENE = "Prolog";
    private const string GAME_SCENE = "Game 1";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }

    public void LoadPrologue()
    {
        SceneManager.LoadScene(PROLOGUE_SCENE);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(GAME_SCENE);
    }

    public void StartGameFlow()
    {
        if (GameData.Instance.prologFinished)
            LoadGame();
        else
            LoadPrologue();
    }

    private void ShowText(string message)
    {
        if (TransitionUI.Instance != null)
            TransitionUI.Instance.Show(message);

        Debug.Log("Showing text");
    }

    /// <summary>
    /// Called when the prologue is completed.
    /// Shows the presentation/loading screen,
    /// preloads the game scene, then switches.
    /// </summary>
    public void CompletePrologue()
    {
        GameData.Instance.prologFinished = true;
        StartCoroutine(ShowSplashThenLoadGame());
    }

    private IEnumerator ShowSplashThenLoadGame()
    {
        ShowText("Adam Al Najar Presents\nZombie Contracting\nLoading Scene...");

    AsyncOperation loadOperation =
        SceneManager.LoadSceneAsync(GAME_SCENE);

    loadOperation.allowSceneActivation = false;

    while (loadOperation.progress < 0.9f)
        yield return null;

    yield return new WaitForSeconds(splashDuration);

    loadOperation.allowSceneActivation = true;
    }
}
