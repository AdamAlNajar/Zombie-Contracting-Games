using UnityEngine;
using System;
using System.IO;
public static class SaveSystem
{
   private static string SavePath =>
        Path.Combine(Application.persistentDataPath, "save.json");

    public static void SaveGame()
    {
        SaveData data = new SaveData
        {
            gameCoins = GameData.Instance.gameCoins,
            prologFinished = GameData.Instance.prologFinished
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);

        Debug.Log("Game Saved: " + SavePath);
    }

    public static void LoadGame()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("No save file found.");
            return;
        }

        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        GameData.Instance.gameCoins = data.gameCoins;
        GameData.Instance.prologFinished = data.prologFinished;

        Debug.Log("Game Loaded");
    }

    public static void DeleteSave()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
        }
    }
}

[Serializable]
public class SaveData
{
    public int gameCoins;
    public bool prologFinished;
}
