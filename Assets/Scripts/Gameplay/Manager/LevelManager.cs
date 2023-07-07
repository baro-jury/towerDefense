using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public static LevelData levelData;
    public static int level;

    public int TotalLives { get; set; }
    public int TotalCoins { get; set; }
    public int CurrentWave { get; set; }

    void MakeSingleInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Awake()
    {
        MakeSingleInstance();

        #region LevelData with one wave
        //List<int> enemies = new List<int>() { 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4 };
        //LevelData lvData = new LevelData(1, 10, 5, enemies); 
        #endregion

        #region LevelData with multiple waves
        //WaveData wave = new WaveData(new List<int>() { 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4 });
        //List<WaveData> waves = new List<WaveData>();
        //waves.Add(wave);
        //waves.Add(wave);
        //LevelData lvData = new LevelData(1, 5, 5, waves); 
        #endregion

        //levelData = lvData;
        //TotalLives = levelData.Health;
        //TotalCoins = levelData.Coins;

        //string json = JsonConvert.SerializeObject(lvData);
        //File.WriteAllText(Application.dataPath + "/Resources/Levels/Level_" + lvData.Level + ".json", json);
    }

    void Start()
    {
        level = PlayerPrefsManager.instance._GetCurrentLevel();
        //CurrentWave = 1;
    }

    public void _Play()
    {
        levelData = _GetLevelData(level);
        TotalLives = levelData.Health;
        TotalCoins = levelData.Coins;
        SceneManager.LoadScene("Gameplay");
    }
    
    public void _PlayLevel(int lv)
    {
        levelData = _GetLevelData(lv);
        TotalLives = levelData.Health;
        TotalCoins = levelData.Coins;
        SceneManager.LoadScene("Gameplay");
    }

    LevelData _GetLevelData(int level)
    {
        var dataStr = Resources.Load("Levels/Level_" + level) as TextAsset;
        return JsonConvert.DeserializeObject<LevelData>(dataStr.text);
    }

}
