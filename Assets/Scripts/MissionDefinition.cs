using UnityEngine;
using System;

/// <summary>
/// Data for a single mission — dialog, enemy count, rewards, etc.
/// </summary>
[Serializable]
public class MissionDefinition
{
    [SerializeField] private string missionName;
    [SerializeField] private string subtitle;
    [SerializeField] private string[] introDialogLines;
    [SerializeField] private string[] completionDialogLines;
    [SerializeField] private int minEnemies;
    [SerializeField] private int maxEnemies;
    [SerializeField] private int coinReward;
    [SerializeField] private int battleIndex; // which wave config to use in EnemySpawner
    [SerializeField] private bool isAvailable = true; // whether this mission can be played yet

    public string MissionName => missionName;
    public string Subtitle => subtitle;
    public string[] IntroDialogLines => introDialogLines;
    public string[] CompletionDialogLines => completionDialogLines;
    public int MinEnemies => minEnemies;
    public int MaxEnemies => maxEnemies;
    public int CoinReward => coinReward;
    public int BattleIndex => battleIndex;
    public bool IsAvailable => isAvailable;

    public MissionDefinition(
        string missionName,
        string subtitle,
        string[] introDialogLines,
        string[] completionDialogLines,
        int minEnemies,
        int maxEnemies,
        int coinReward,
        int battleIndex = 0,
        bool isAvailable = true
    )
    {
        this.missionName = missionName;
        this.subtitle = subtitle;
        this.introDialogLines = introDialogLines;
        this.completionDialogLines = completionDialogLines;
        this.minEnemies = minEnemies;
        this.maxEnemies = maxEnemies;
        this.coinReward = coinReward;
        this.battleIndex = battleIndex;
        this.isAvailable = isAvailable;
    }
}
