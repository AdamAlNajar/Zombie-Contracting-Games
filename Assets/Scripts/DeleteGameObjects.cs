using UnityEngine;

public class DeleteGameObjects : MonoBehaviour
{
    public GameObject[] toDestroy;
    public ButtonScriptable starterButton;
    public void Start()
    {
        if(GameData.Instance.level1SeriesOfEventsCompleted == true)
        {
            Destroy(starterButton.gameObject);
        }
    }
    public void destoryMultipleObjectsAtOnce()
    {
        for (int i = 0; i < toDestroy.Length; i++)
        {
            Destroy(toDestroy[i]);
        }
    }
}
