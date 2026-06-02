using UnityEngine;

public class GameData : MonoBehaviour
{
    public int gameCoins;
    public static GameData Instance;
    public bool prologFinished;

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

}