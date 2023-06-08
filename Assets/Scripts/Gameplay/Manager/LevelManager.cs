using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private int lives = 10;
    public int TotalLives { get; set; }
    public int CurrentWave { get; set; }

    void MakeInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        TotalLives = lives;
        CurrentWave = 1;
    }

    private void ReduceLives(Enemy enemy)
    {
        TotalLives--;
        if (TotalLives <= 0)
        {
            TotalLives = 0;
            GameOver();
        }
    }
    private void GameOver()
    {

    }

    private void WaveCompleted()
    {

    }

    private void OnEnable()
    {
        Enemy.OnEndReached += ReduceLives;
        //Spawner.OnWaveCompleted += WaveCompleted;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= ReduceLives;


    }
}
