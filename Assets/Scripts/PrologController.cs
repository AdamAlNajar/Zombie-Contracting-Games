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
                "Coach : Listen up, rookie. WASD or arrow keys to move. Left-click to advance dialog. Got it?",
                "Coach : Your weapon follows your mouse — aim at the target and left-click to fire. Don't waste bullets.",
                "Coach : See ammo boxes scattered around? Walk over 'em with the right gun equipped to grab extra rounds.",
                "Coach : Alright, that's the basics. The cave ahead is infested — show me you've got what it takes. Move out."
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
