using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class LevelData
{
    private int level;
    //private string difficulty;
    private int coins;
    private int health;
    private List<int> enemies;
    //private List<WaveData> waves;

    public LevelData() { }

    public LevelData(int level, int coins, int health, List<int> enemies)
    {
        this.level = level;
        this.coins = coins;
        this.health = health;
        this.enemies = enemies;
    }

    //public LevelData(int level, int coins, int health, List<WaveData> waves)
    //{
    //    this.level = level;
    //    this.coins = coins;
    //    this.health = health;
    //    this.waves = waves;
    //}

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    //public string Difficulty
    //{
    //    get { return difficulty; }
    //    set { difficulty = value; }
    //}

    public int Coins
    {
        get { return coins; }
        set { coins = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public List<int> Enemies
    {
        get { return enemies; }
        set { enemies = value; }
    }

    //public List<WaveData> Waves
    //{
    //    get { return waves; }
    //    set { waves = value; }
    //}
}

public class WaveData
{
    public List<int> EnemyIndexes { get; set; }

    public WaveData() { }

    public WaveData(List<int> enemyIndexes)
    {
        EnemyIndexes = enemyIndexes;
    }
}