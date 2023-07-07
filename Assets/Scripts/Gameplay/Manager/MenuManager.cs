using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField]
    private AudioClip clickButtonClip, switchClip;
    [SerializeField]
    private Text levelText;
    [SerializeField]
    private GameObject settingPanel, guidePanel;
    [SerializeField]
    private Button playButton, settingButton, soundOnButton, soundOffButton, musicOnButton, musicOffButton;

    void _MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Awake()
    {
        _MakeInstance();
    }

    void Start()
    {
        ConfigForButtons();
        ConfigForFields();
    }

    void ConfigForButtons()
    {
        playButton.onClick.AddListener(_PlayGame);
        settingButton.onClick.AddListener(_GoToSetting);

        soundOffButton.gameObject.SetActive(AudioManager.instance.soundSource.mute);
        musicOffButton.gameObject.SetActive(AudioManager.instance.musicSource.mute);

        soundOnButton.onClick.AddListener(_TurnOffSound);
        soundOffButton.onClick.AddListener(_TurnOnSound);
        musicOnButton.onClick.AddListener(_TurnOffMusic);
        musicOffButton.onClick.AddListener(_TurnOnMusic);
    }

    void ConfigForFields()
    {
        levelText.text = "LEVEL " + PlayerPrefsManager.instance._GetCurrentLevel();
    }

    void MakeImageTransparent(Image image)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }
    void MakeTextTransparent(Text text)
    {

    }

    public void _PlayGame()
    {
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        LevelManager.instance._Play();
        //LevelManager.instance._PlayLevel(PlayerPrefsManager.instance._GetCurrentLevel());
    }

    public void _GoToSetting()
    {
        AudioManager.instance.soundSource.PlayOneShot(clickButtonClip);
        MakeImageTransparent(settingPanel.transform.GetComponent<Image>());
        settingPanel.transform.GetChild(0).localScale = Vector3.zero;
        settingPanel.SetActive(true);
        settingPanel.transform.GetComponent<Image>().DOFade(.5f, .25f).SetEase(Ease.InOutQuad).SetUpdate(true);
        settingPanel.transform.GetChild(0).DOScale(Vector3.one, .25f).SetEase(Ease.InOutQuad).SetUpdate(true);
    }

    public void _TurnOffSound()
    {
        AudioManager.instance.soundSource.mute = true;
        soundOffButton.gameObject.SetActive(true);
    }

    public void _TurnOnSound()
    {
        AudioManager.instance.soundSource.mute = false;
        AudioManager.instance.soundSource.PlayOneShot(switchClip);
        soundOffButton.gameObject.SetActive(false);
    }

    public void _TurnOffMusic()
    {
        AudioManager.instance.soundSource.PlayOneShot(switchClip);
        AudioManager.instance.musicSource.mute = true;
        musicOffButton.gameObject.SetActive(true);
    }

    public void _TurnOnMusic()
    {
        AudioManager.instance.soundSource.PlayOneShot(switchClip);
        AudioManager.instance.musicSource.mute = false;
        musicOffButton.gameObject.SetActive(false);
    }

    public void _TurnOffSettingPanel()
    {
        settingPanel.SetActive(false);
    }

}
