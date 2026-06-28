using UnityEngine;

public class Mission1EventManager : MonoBehaviour
{
    public Dialog dialog;
    public EnemySpawner eSpawner;
    public void OnMissionCompleted() {
        GameData.Instance.gameCoins += Random.Range(10, 51);
    }

    public void Start()
    {
        dialog.dialogLines = new string[]
        {
                "Welcome To your first mission soilder.",
                "You will soon face a wave of zombies and other creatures..",
                "This is Lettol city, Grore's capital. it was once a very beautiful city...",
                "Now it has become infested by zombies",
                "Kill all of them, for Grore.",
                "Battle Commencing"
        };

        // IMPORTANT: Activate textBox BEFORE starting dialog
        dialog.textBox.SetActive(true);

        dialog.StartDialog();
    }

    public void Update()
    {
        if (!dialog.gameObject.activeSelf)
        {
            eSpawner.SummonEnemies();
        }
    }
}
