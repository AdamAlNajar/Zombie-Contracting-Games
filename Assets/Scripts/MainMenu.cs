using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayTutorial()
    {
        SceneManager.LoadScene("Prolog");
    }
    public void Play()
    {
        SceneManager.LoadScene("Game 1");
    }
    public void Quit()
    {
        Application.Quit();
    }
}