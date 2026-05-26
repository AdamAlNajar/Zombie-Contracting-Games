using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager : MonoBehaviour
{
    public GameObject splashScreen;
    public static SceneFlowManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void HandleAllEnemiesDefeated()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Prolog")
        {
            StartCoroutine(ShowSplashThenLoadMenu());
        }
    }

    private IEnumerator ShowSplashThenLoadMenu()
    {
        splashScreen.SetActive(true);

        yield return new WaitForSeconds(3f);

        splashScreen.SetActive(false);

        SceneManager.LoadScene("MainMenu");
    }

    // 🔥 THIS IS THE IMPORTANT ADDITION
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            SetMainMenuState(true);
        }
        else if (scene.name == "Prolog")
        {
            SetMainMenuState(false);
        }
    }

    // keep your variable name concept
    public GameObject[] gameSceneObjects;

    private void SetMainMenuState(bool isMenu)
    {
        if (gameSceneObjects == null) return;

        for (int i = 0; i < gameSceneObjects.Length; i++)
        {
            if (gameSceneObjects[i] != null)
            {
                gameSceneObjects[i].SetActive(!isMenu);
            }
        }
    }
}
