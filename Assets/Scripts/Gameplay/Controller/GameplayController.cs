using DG.Tweening;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    [Header("Settings for Turret")]
    public GameObject turretContainer;
    public List<Button> btnsSetupTurret;
    public List<GameObject> turretsBuild;
    public List<GameObject> turretsUpgrade;
    public List<AudioClip> basicTurretClips;
    public List<AudioClip> advancedTurretClips;
    public AudioClip setUpTurretClip, buildTurretClip, upgradeTurretClip, sellTurretClip;
    [SerializeField]
    private GameObject buildTurretPanel, adjustTurretPanel;
    [SerializeField]
    private Button singleBarrelledButton, doubleBarrelledButton, rocketLauncherButton, upgradeButton, sellButton;
    public Text upgradeTurretCoinText, sellTurretCoinText;

    [Header("UI")]
    [SerializeField]
    private List<Button> homeBtns;
    [SerializeField]
    private List<Text> levelTexts;

    public AudioClip clickButtonClip, switchClip, passLevelClip;
    //singleBulletClip, singleLaserClip, doubleBulletClip, doubleLaserClip, rocketClip, doubleRocketClip

    //public Transform startCoinAnimPos, endCoinAnimPos, rewardPos;
    public Text healthText, coinText, waveText, turretGearText;

    [SerializeField]
    private GameObject pausePanel, quitPanel;
    [SerializeField]
    private Button btNextLevel, btReplay,
        settingOnButton, settingOffButton, soundOnButton, soundOffButton, musicOnButton, musicOffButton, quitButton, quitConfirmButton;
    [SerializeField]
    private GameObject gameOverPanel, winPanel;
    [SerializeField]
    private Slider progressSlider;

    private float progressValue;
    private Turret _turret;

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        ConfigForButtons();
        ConfigForFields();
        ConfigForTurrets();
        ConfigForTurretPlaces();
    }

    void Update()
    {
        singleBarrelledButton.interactable = LevelManager.instance.TotalCoins switch
        {
            >= 10 => true,
            _ => false,
        };

        doubleBarrelledButton.interactable = LevelManager.instance.TotalCoins switch
        {
            >= 15 => true,
            _ => false,
        };

        rocketLauncherButton.interactable = LevelManager.instance.TotalCoins switch
        {
            >= 20 => true,
            _ => false,
        };
    }

    #region Configuration
    void ConfigForButtons()
    {
        singleBarrelledButton.interactable = false;
        doubleBarrelledButton.interactable = false;
        rocketLauncherButton.interactable = false;

        buildTurretPanel.GetComponent<Button>().onClick.AddListener(delegate
        {
            buildTurretPanel.transform.GetChild(0).GetComponent<RectTransform>().DOScale(Vector3.zero, .25f)
            .SetEase(Ease.InOutQuad).SetUpdate(true).OnComplete(() =>
            {
                buildTurretPanel.SetActive(false);
                buildTurretPanel.transform.GetChild(0).localScale = Vector3.one;
            });
        });
        adjustTurretPanel.GetComponent<Button>().onClick.AddListener(delegate
        {
            adjustTurretPanel.transform.GetChild(0).GetComponent<RectTransform>().DOScale(Vector3.zero, .25f)
            .SetEase(Ease.InOutQuad).SetUpdate(true).OnComplete(() =>
            {
                adjustTurretPanel.SetActive(false);
                adjustTurretPanel.transform.GetChild(0).localScale = Vector3.one;
            });
        });

        soundOffButton.gameObject.SetActive(AudioManager.instance.soundSource.mute);
        musicOffButton.gameObject.SetActive(AudioManager.instance.musicSource.mute);

        settingOnButton.onClick.AddListener(_Pause);
        settingOffButton.onClick.AddListener(_Resume);
        pausePanel.GetComponent<Button>().onClick.AddListener(_Resume);
        soundOnButton.onClick.AddListener(_TurnOffSound);
        soundOffButton.onClick.AddListener(_TurnOnSound);
        musicOnButton.onClick.AddListener(_TurnOffMusic);
        musicOffButton.onClick.AddListener(_TurnOnMusic);
        quitButton.onClick.AddListener(_QuitConfirmation);
        quitConfirmButton.onClick.AddListener(_GoToHome);

        btNextLevel.onClick.AddListener(_GoToNextLevel);
        btReplay.onClick.AddListener(_Replay);
        foreach (Button btn in homeBtns)
        {
            btn.onClick.AddListener(_GoToHome);
        }

    }

    void ConfigForFields()
    {
        foreach (Text text in levelTexts)
        {
            text.text = "LEVEL " + PlayerPrefsManager.instance._GetCurrentLevel();
        }

        coinText.text = LevelManager.instance.TotalCoins + "";
        healthText.text = LevelManager.instance.TotalLives + "";
        progressSlider.value = 0;
        progressValue = 1.0f / Spawner.instance.EnemyCount;

        Time.timeScale = 1;
        soundOnButton.transform.GetChild(1).gameObject.SetActive(!AudioManager.instance.soundSource.mute);
        musicOnButton.transform.GetChild(1).gameObject.SetActive(!AudioManager.instance.musicSource.mute);
    }

    void ConfigForTurrets()
    {
        //singleBarrelledTurret
        turretsBuild[0].transform.GetComponent<Turret>().CoinBuy = 10;
        turretsBuild[0].transform.GetComponent<Turret>().CoinUpgrade = 10 * 2;
        turretsBuild[0].transform.GetComponent<Turret>().CoinSell = 10 / 2;

        //doubleBarrelledTurret
        turretsBuild[1].transform.GetComponent<Turret>().CoinBuy = 15;
        turretsBuild[1].transform.GetComponent<Turret>().CoinUpgrade = 15 * 2;
        turretsBuild[1].transform.GetComponent<Turret>().CoinSell = 15 / 2;

        //rocketLauncher
        turretsBuild[2].transform.GetComponent<Turret>().CoinBuy = 20;
        turretsBuild[2].transform.GetComponent<Turret>().CoinUpgrade = 20 * 2;
        turretsBuild[2].transform.GetComponent<Turret>().CoinSell = 20 / 2;

    }

    void ConfigForTurretPlaces()
    {
        for (int i = 0; i < btnsSetupTurret.Count; i++)
        {
            //var obj = new GameObject("TurretContainer" + (i + 1));
            var obj = Instantiate(turretContainer);
            obj.transform.position = new Vector3
                (btnsSetupTurret[i].transform.position.x, btnsSetupTurret[i].transform.position.y);
            btnsSetupTurret[i].onClick.AddListener(delegate { _SetupTurret(obj.GetComponent<TurretContainer>()); });
        }
    }

    #endregion

    #region Setup Turret
    public void _SetupTurret(TurretContainer turretContainer)
    {
        AudioManager.instance.soundSource.PlayOneShot(setUpTurretClip);
        if (turretContainer.transform.childCount == 0)
        {
            OpenBuildPanel(turretContainer);
        }
        else
        {
            OpenAdjustPanel(turretContainer);
        }
    }
    void OpenBuildPanel(TurretContainer turretContainer)
    {
        buildTurretPanel.SetActive(true);

        singleBarrelledButton.onClick.RemoveAllListeners();
        doubleBarrelledButton.onClick.RemoveAllListeners();
        rocketLauncherButton.onClick.RemoveAllListeners();

        singleBarrelledButton.onClick.AddListener(delegate
        {
            int index = singleBarrelledButton.transform.GetSiblingIndex();
            _turret = turretsBuild[index].transform.GetComponent<Turret>();

            turretContainer.TurretIndex = singleBarrelledButton.transform.GetSiblingIndex();
            _BuildTurret(turretContainer);
        });

        doubleBarrelledButton.onClick.AddListener(delegate
        {
            int index = doubleBarrelledButton.transform.GetSiblingIndex();
            _turret = turretsBuild[index].transform.GetComponent<Turret>();

            turretContainer.TurretIndex = doubleBarrelledButton.transform.GetSiblingIndex();
            _BuildTurret(turretContainer);
        });

        rocketLauncherButton.onClick.AddListener(delegate
        {
            int index = rocketLauncherButton.transform.GetSiblingIndex();
            _turret = turretsBuild[index].transform.GetComponent<Turret>();

            turretContainer.TurretIndex = rocketLauncherButton.transform.GetSiblingIndex();
            _BuildTurret(turretContainer);
        });

    }
    public void _BuildTurret(TurretContainer turretContainer)
    {
        int index = turretContainer.TurretIndex;
        if (_turret.CoinBuy <= LevelManager.instance.TotalCoins)
        {
            AudioManager.instance.soundSource.PlayOneShot(buildTurretClip);

            GameObject turret = Instantiate(turretsBuild[index], turretContainer.transform);
            turret.GetComponent<Turret>().CoinBuy = _turret.CoinBuy;
            turret.GetComponent<Turret>().CoinUpgrade = _turret.CoinUpgrade;
            turret.GetComponent<Turret>().CoinSell = _turret.CoinSell;
            turret.GetComponent<TurretProjectile>().ProjectileClip = basicTurretClips[index];
            turretContainer.Gear = 1;
            turretContainer.CanUpgrade = true;
            buildTurretPanel.SetActive(false);

            LevelManager.instance.TotalCoins -= _turret.CoinBuy;
            coinText.text = LevelManager.instance.TotalCoins + "";
        }

    }

    void OpenAdjustPanel(TurretContainer turretContainer)
    {
        _turret = turretContainer.transform.GetChild(0).GetComponent<Turret>();
        //_turret = turretsBuild[turretContainer.GetComponent<TurretContainer>().TurretIndex].GetComponent<Turret>();
        //Debug.Log(_turret.CoinBuy + " - " + _turret.CoinUpgrade + " - " + _turret.CoinSell);

        turretGearText.text = "GEAR " + turretContainer.Gear;
        upgradeTurretCoinText.text = _turret.CoinUpgrade + "";
        sellTurretCoinText.text = _turret.CoinSell + "";
        adjustTurretPanel.SetActive(true);

        upgradeButton.onClick.RemoveAllListeners();
        sellButton.onClick.RemoveAllListeners();

        if (LevelManager.instance.TotalCoins >= _turret.CoinUpgrade
            && turretContainer.CanUpgrade)
        {
            upgradeButton.interactable = true;
            upgradeButton.onClick.AddListener(delegate { _UpgradeTurret(turretContainer); });
        }
        else
        {
            upgradeButton.interactable = false;
        }
        sellButton.onClick.AddListener(delegate { _SellTurret(turretContainer); });
    }
    public void _UpgradeTurret(TurretContainer turretContainer)
    {
        AudioManager.instance.soundSource.PlayOneShot(upgradeTurretClip);

        int index = turretContainer.TurretIndex;
        Destroy(turretContainer.transform.GetChild(0).gameObject);
        GameObject turret = Instantiate(turretsUpgrade[index], turretContainer.transform);
        turret.GetComponent<Turret>().CoinBuy = _turret.CoinUpgrade;
        turret.GetComponent<Turret>().CoinUpgrade = _turret.CoinUpgrade * 2;
        turret.GetComponent<Turret>().CoinSell = _turret.CoinBuy;
        turret.GetComponent<TurretProjectile>().ProjectileClip = advancedTurretClips[index];
        turretContainer.Gear++;
        turretContainer.CanUpgrade = false;
        adjustTurretPanel.SetActive(false);

        LevelManager.instance.TotalCoins -= _turret.CoinUpgrade;
        coinText.text = LevelManager.instance.TotalCoins + "";
    }
    public void _SellTurret(TurretContainer turretContainer)
    {
        AudioManager.instance.soundSource.PlayOneShot(sellTurretClip);

        LevelManager.instance.TotalCoins += _turret.CoinSell;
        coinText.text = LevelManager.instance.TotalCoins + "";
        Destroy(turretContainer.transform.GetChild(0).gameObject);
        adjustTurretPanel.SetActive(false);
    }
    #endregion

    #region Interaction
    void PauseTime()
    {
        //AudioManager.instance.supporterSource.Pause();
        //AudioManager.instance.timeWarningSource.Pause();
        Time.timeScale = 0;
    }

    void UnpauseTime()
    {
        //AudioManager.instance.supporterSource.UnPause();
        //AudioManager.instance.timeWarningSource.UnPause();
        Time.timeScale = 1;
    }

    void MakeImageTransparent(Image image)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }

    void MakeTextTransparent(Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }

    public void _Pause()
    {
        PauseTime();
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        settingOnButton.interactable = false;
        MakeImageTransparent(pausePanel.transform.GetComponent<Image>());
        MakeTextTransparent(settingOffButton.transform.GetChild(0).GetComponent<Text>());
        MakeTextTransparent(soundOnButton.transform.GetChild(1).GetComponent<Text>());
        MakeTextTransparent(musicOnButton.transform.GetChild(1).GetComponent<Text>());
        MakeTextTransparent(quitButton.transform.GetChild(0).GetComponent<Text>());

        pausePanel.SetActive(true);
        pausePanel.GetComponent<Button>().interactable = false;
        pausePanel.transform.GetComponent<Image>().DOFade(.5f, .5f).SetEase(Ease.InOutQuad).SetUpdate(true);

        settingOffButton.transform.DORotate(new Vector3(0, 0, 0), .5f).SetEase(Ease.InOutQuad).SetUpdate(true);

        soundOnButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-70, -65, 0);
        musicOnButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-70, -65, 0);
        quitButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-70, -65, 0);

        soundOnButton.GetComponent<RectTransform>().DOAnchorPosY(-215, .4f).SetEase(Ease.InOutQuad).SetUpdate(true);
        musicOnButton.GetComponent<RectTransform>().DOAnchorPosY(-365, .4f).SetEase(Ease.InOutQuad).SetUpdate(true);
        quitButton.GetComponent<RectTransform>().DOAnchorPosY(-515, .4f).SetEase(Ease.InOutQuad).SetUpdate(true)
            .OnComplete(() =>
            {
                settingOffButton.interactable = true;
                pausePanel.GetComponent<Button>().interactable = true;

                soundOnButton.transform.GetChild(1).GetComponent<Text>().DOFade(1f, 0.1f).SetEase(Ease.InOutQuad).SetUpdate(true);
                musicOnButton.transform.GetChild(1).GetComponent<Text>().DOFade(1f, 0.1f).SetEase(Ease.InOutQuad).SetUpdate(true);
                quitButton.transform.GetChild(0).GetComponent<Text>().DOFade(1f, 0.1f).SetEase(Ease.InOutQuad).SetUpdate(true);
                settingOffButton.transform.GetChild(0).GetComponent<Text>().DOFade(1f, 0.1f).SetEase(Ease.InOutQuad).SetUpdate(true);
            });
    }

    public void _Resume()
    {
        UnpauseTime();
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        settingOffButton.interactable = false;
        pausePanel.GetComponent<Button>().interactable = false;

        soundOnButton.transform.GetChild(1).GetComponent<Text>().DOFade(0f, 0.1f).SetEase(Ease.InOutQuad).SetUpdate(true);
        musicOnButton.transform.GetChild(1).GetComponent<Text>().DOFade(0f, 0.1f).SetEase(Ease.InOutQuad).SetUpdate(true);
        quitButton.transform.GetChild(0).GetComponent<Text>().DOFade(0f, 0.1f).SetEase(Ease.InOutQuad).SetUpdate(true);
        settingOffButton.transform.GetChild(0).GetComponent<Text>().DOFade(0f, 0.1f).SetEase(Ease.InOutQuad).SetUpdate(true);

        pausePanel.transform.GetComponent<Image>().DOFade(0f, .4f).SetEase(Ease.InOutQuad).SetUpdate(true);

        settingOffButton.transform.DORotate(new Vector3(0, 0, 180), .5f).SetEase(Ease.InOutQuad).SetUpdate(true);

        soundOnButton.GetComponent<RectTransform>().DOAnchorPosY(-65, .4f).SetEase(Ease.InOutQuad).SetUpdate(true);
        musicOnButton.GetComponent<RectTransform>().DOAnchorPosY(-65, .4f).SetEase(Ease.InOutQuad).SetUpdate(true);
        quitButton.GetComponent<RectTransform>().DOAnchorPosY(-65, .4f).SetEase(Ease.InOutQuad).SetUpdate(true)
            .OnComplete(() =>
            {
                pausePanel.SetActive(false);
                settingOnButton.interactable = true;
            });
    }

    public void _TurnOffSound()
    {
        AudioManager.instance.soundSource.mute = true;
        //AudioManager.instance.timeWarningSource.mute = true;
        soundOffButton.gameObject.SetActive(true);
        soundOnButton.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void _TurnOnSound()
    {
        AudioManager.instance.soundSource.mute = false;
        //AudioManager.instance.timeWarningSource.mute = false;
        AudioManager.instance.soundSource.PlayOneShot(switchClip);
        soundOffButton.gameObject.SetActive(false);
        soundOnButton.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void _TurnOffMusic()
    {
        AudioManager.instance.soundSource.PlayOneShot(switchClip);
        AudioManager.instance.musicSource.mute = true;
        musicOffButton.gameObject.SetActive(true);
        musicOnButton.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void _TurnOnMusic()
    {
        AudioManager.instance.soundSource.PlayOneShot(switchClip);
        AudioManager.instance.musicSource.mute = false;
        musicOffButton.gameObject.SetActive(false);
        musicOnButton.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void _QuitConfirmation()
    {
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        quitPanel.transform.GetComponent<Image>().color = new Color
            (quitPanel.transform.GetComponent<Image>().color.r,
            quitPanel.transform.GetComponent<Image>().color.g,
            quitPanel.transform.GetComponent<Image>().color.b, 0);
        quitPanel.transform.GetChild(0).GetComponent<RectTransform>().localScale = Vector3.zero;
        quitPanel.SetActive(true);
        quitPanel.transform.GetComponent<Image>().DOFade(.5f, .25f).SetEase(Ease.InOutQuad).SetUpdate(true);
        quitPanel.transform.GetChild(0).GetComponent<RectTransform>().DOScale(Vector3.one, .25f).SetEase(Ease.InOutQuad).SetUpdate(true);
    }

    public void _GoToHome()
    {
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        SceneManager.LoadScene("Home");
    }

    public void _GameOver()
    {
        Time.timeScale = 0;
        MakeImageTransparent(gameOverPanel.transform.GetComponent<Image>());
        gameOverPanel.transform.GetChild(0).GetComponent<RectTransform>().localScale = Vector3.zero;
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.GetComponent<Image>().DOFade(.5f, .25f).SetEase(Ease.InOutQuad).SetUpdate(true);
        gameOverPanel.transform.GetChild(0).GetComponent<RectTransform>().DOScale(Vector3.one, .25f).SetEase(Ease.InOutQuad).SetUpdate(true);
    }

    public void _Replay()
    {
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        LevelManager.instance._Play();
        //LevelManager.instance._PlayLevel(LevelManager.level);
    }

    public void _CompleteLevel()
    {
        Time.timeScale = 0;
        AudioManager.instance.soundSource.Stop();

        if (PlayerPrefsManager.instance._GetCurrentLevel() < Resources.LoadAll("Levels").Length)
        {
            PlayerPrefsManager.instance._SetCurrentLevel(PlayerPrefsManager.instance._GetCurrentLevel() + 1);
        }
        
        MakeImageTransparent(winPanel.transform.GetComponent<Image>());
        winPanel.transform.GetChild(0).GetComponent<RectTransform>().localScale = Vector3.zero;
        winPanel.SetActive(true);
        winPanel.transform.GetComponent<Image>().DOFade(.5f, .25f).SetEase(Ease.InOutQuad).SetUpdate(true);
        winPanel.transform.GetChild(0).GetComponent<RectTransform>().DOScale(Vector3.one, .25f).SetEase(Ease.InOutQuad).SetUpdate(true);

    }

    public void _GoToNextLevel()
    {
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        if (LevelManager.level == Resources.LoadAll("Levels").Length)
        {
            _GoToHome();
        }
        else
        {
            LevelManager.level = PlayerPrefsManager.instance._GetCurrentLevel();
            LevelManager.instance._Play();
            //LevelManager.instance._PlayLevel(LevelManager.level);
        }
    }

    #endregion

    #region Ingame
    private void ReduceLives(Enemy enemy)
    {
        LevelManager.instance.TotalLives--;
        healthText.text = LevelManager.instance.TotalLives + "";
        if (LevelManager.instance.TotalLives <= 0)
        {
            LevelManager.instance.TotalLives = 0;
            _GameOver();
        }
        //EnemyDisappeared(enemy);
    }

    private void RewardCoins(Enemy enemy)
    {
        LevelManager.instance.TotalCoins += enemy.DeathCoinReward;
        coinText.text = LevelManager.instance.TotalCoins + "";
        //EnemyDisappeared(enemy);
    }

    private void EnemyAppeared()
    {
        progressSlider.value += progressValue;
    }

    private void EnemyDisappeared(Enemy enemy)
    {
        if (Spawner.instance.NoEnemies())
        {
            _CompleteLevel();
        }
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += ReduceLives;
        Projectile.OnEnemyDead += RewardCoins;
        Spawner.OnSpawnEnemy += EnemyAppeared;
        //Spawner.OnWaveCompleted += WaveCompleted;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= ReduceLives;
        Projectile.OnEnemyDead -= RewardCoins;
        Spawner.OnSpawnEnemy -= EnemyAppeared;
    }
    #endregion

}
