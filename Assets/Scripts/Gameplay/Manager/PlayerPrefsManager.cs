using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public static PlayerPrefsManager instance;

    private const string LEVEL = "Level";

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


}
