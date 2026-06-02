using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager : MonoBehaviour
{
    public static SceneFlowManager Instance;

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
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadPrologue()
    {
        SceneManager.LoadScene("Prolog");
    }

    public void LoadGame()
    {
        // For now
        SceneManager.LoadScene("Game 1");
    }

    public void StartGameFlow()
    {
        if (GameData.Instance.prologFinished)
            LoadGame();
        else
            LoadPrologue();
    }
}
