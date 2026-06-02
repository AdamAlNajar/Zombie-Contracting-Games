using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public void OnPlayPressed()
    {
        SceneFlowManager.Instance.StartGameFlow();
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }
}