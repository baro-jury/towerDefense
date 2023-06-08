using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public static PlayerPrefsManager instance;

    private const string LEVEL = "Level";
    private const string COINS = "Coins";

    void _MakeSingleInstance()
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

    void _CheckFirstTimePlayGame()
    {
        if (!PlayerPrefs.HasKey("_CheckFirstTimePlayGame"))
        {
            PlayerPrefs.SetInt(LEVEL, 1);
            //PlayerPrefs.SetInt(COINS, 200);
            PlayerPrefs.SetInt(COINS, 0999999999);
            PlayerPrefs.SetInt("_CheckFirstTimePlayGame", 0);
        }
    }

    void Awake()
    {
        _MakeSingleInstance();
        _CheckFirstTimePlayGame();
        //_SetCoinsInPossession(9999999, true);
        //_SetCurrentLevel(5);
    }

    public int _GetCurrentLevel()
    {
        return PlayerPrefs.GetInt(LEVEL);
    }

    public void _SetCurrentLevel(int level)
    {
        PlayerPrefs.SetInt(LEVEL, level);
    }

    public int _GetCoinsInPossession()
    {
        return PlayerPrefs.GetInt(COINS);
    }

    public void _SetCoinsInPossession(int coin, bool isEarning)
    {
        int temp = -1;
        if (isEarning) temp = 1;
        if (PlayerPrefs.GetInt(COINS) + temp * coin > 0)
            PlayerPrefs.SetInt(COINS, PlayerPrefs.GetInt(COINS) + temp * coin);
        else
            PlayerPrefs.SetInt(COINS, 0);
    }

}
