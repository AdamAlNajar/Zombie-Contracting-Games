using UnityEngine;

public class PrologController : MonoBehaviour
{
    public void OnPrologueCompleted()
    {
        SceneFlowManager.Instance.CompletePrologue();
    }
}
