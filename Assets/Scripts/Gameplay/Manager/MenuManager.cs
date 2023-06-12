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
    private GameObject menuPanel, settingPanel, guidePanel;
    [SerializeField]
    private Text highscore;
    [SerializeField]
    private AudioClip clickButtonClip, switchClip;

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

    public void _PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void _BackToMenu()
    {
        AudioManager.instance.audioSource.PlayOneShot(clickButtonClip);
        menuPanel.SetActive(true);
    }

    public void _GoToSetting()
    {
        AudioManager.instance.audioSource.PlayOneShot(clickButtonClip);
        settingPanel.SetActive(true);
    }

    public void _TurnOffSound()
    {
        AudioManager.instance.audioSource.mute = true;
        AudioManager.instance.audioSource.PlayOneShot(switchClip);
        settingPanel.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
        //              setting form         body        middle      sound on   sound off
    }

    public void _TurnOnSound()
    {
        AudioManager.instance.audioSource.mute = false;
        AudioManager.instance.audioSource.PlayOneShot(switchClip);
        settingPanel.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
        //              setting form         body        middle      sound on   sound off
    }

    public void _TurnOffMusic()
    {
        AudioManager.instance.audioSource.PlayOneShot(switchClip);
        AudioManager.instance.musicSource.mute = true;
        settingPanel.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(true);
        //              setting form         body        middle      music on   music off
    }

    public void _TurnOnMusic()
    {
        AudioManager.instance.audioSource.PlayOneShot(switchClip);
        AudioManager.instance.musicSource.mute = false;
        settingPanel.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(false);
        //              setting form         body        middle      music on   music off
    }

    public void _TurnOffSettingPanel()
    {
        settingPanel.SetActive(false);
    }

}
