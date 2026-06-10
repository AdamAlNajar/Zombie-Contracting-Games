using UnityEngine;

public class PrologController : MonoBehaviour
{
    public Dialog dialog;

    void Start()
    {
        LoadPrologTutorial();
    }
    public void LoadPrologTutorial()
    {
        if(SceneFlowManager.Instance.prologStarted == true)
        {
            dialog.dialogLines = new string[]
            {
                "Coach : To move around the game use arrow keys or WASD. \n To progress through this and any dialog press the Left mouse button",
                "Coach : To shoot aim the barrel to the target using mouse and shoot using the left mouse button",
                "Coach : You can collect extra ammo for a specific gun \n by going near ammo boxes with the gun selected",
                "Coach : Pretty much it. go into the cave and show me what you have got"
            };
            dialog.textBox.SetActive(true);
            dialog.StartDialog();
        }
    }
    public void OnPrologueCompleted()
    {
        SceneFlowManager.Instance.CompletePrologue();
        SceneFlowManager.Instance.prologStarted = false;
    }
}
