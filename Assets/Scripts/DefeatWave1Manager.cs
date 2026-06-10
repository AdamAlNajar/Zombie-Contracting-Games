using UnityEngine;

public class DefeatWave1Manager : MonoBehaviour
{
    public Dialog dialog;

    public void OnDefeat()
    {
        dialog.dialogLines = new string[]
        {
            "??? : Good Job on killing the enemies my brother",
            "You : Who is this",
            "Contractor : Im the one who assigned you to this task, The contractor",
            "Contractor : Im the one who gives you money, for every enemy you kill",
            "Contractor : Im the one who gives you money for every wave of enemies you destory",
            "Adam Al Najar (Dev) : Welcome to Zombie contracting simulator.. ",
            "Adam Al Najar (Dev) : This games aim is to kill all zombies in this (WORLD) and let citizens live happily without fear",
            "Adam Al Najar (Dev) : You accomplish this by doing contracts that reward you with money that can be used to upgrade/ buy \n new weapons and other stuff that will help you perform \n your tasks to the best of your ability",
            "Adam Al Najar (Dev) : You will be able to see all contracts after closing this dialog, \n they will be in the form of pressable buttons like always.",
            "Adam Al Najar (Dev) : Thank you for playing, lets start."

            // Add Buttons That take to contact scenes
        };
        dialog.textBox.SetActive(true);
        dialog.StartDialog();
        GameData.Instance.level1SeriesOfEventsCompleted = true;
        SaveSystem.SaveGame();
        Debug.Log("Saved Status");
    }
}
