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
    }
    

    public void HandleAllEnemiesDefeated()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Prolog")
        {
            StartCoroutine(ShowSplashThenLoadMenu());
        }
    }

    private System.Collections.IEnumerator ShowSplashThenLoadMenu()
    {
        splashScreen.SetActive(true);

        yield return new WaitForSeconds(3f);
        splashScreen.SetActive(false);

        SceneManager.LoadScene("MainMenu");
    }
}
