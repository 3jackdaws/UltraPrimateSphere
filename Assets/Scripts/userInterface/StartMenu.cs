using System;
using UnityEngine;
using System.Collections;
using System.Xml.Schema;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class StartMenu : MonoBehaviour
{
    public RectTransform titleCard;
    public RectTransform mainMenuCard;
    public RectTransform settingsCard;
    public RectTransform loadingCard;

    public AudioSource audio_player;
    public AudioSource SFX;
    public AudioClip select;
    public AudioClip move;
    
    public Scene level1;
    public string levelName;
    
    AsyncOperation async;
    public EventSystem controller;
    private float audio_volume;
    private float camera_sensitivity;
    private int smb_controls;

    public Text sensitivity_text;

    private String playerSensitivityPreferenceKeyName = "CameraSensitivity";
    private String playerMusicVolumePreferenceKeyName = "MusicVolume";
    private String playerSMBControlPreferenceKey = "SMBControls";
    
    // Use this for initialization
    void Start ()
	{
        //Load player preferences
        float localAudioVolume = -1;
        float localCameraSensitivity = -1;
        try
        {
            localAudioVolume = PlayerPrefs.GetFloat(playerMusicVolumePreferenceKeyName);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Setting player audio preference to default value: 0.7 ");
            localAudioVolume = 0.7f;
            PlayerPrefs.SetFloat(playerMusicVolumePreferenceKeyName, 0.7f);
        }
        try
        {
            localCameraSensitivity = PlayerPrefs.GetFloat(playerSensitivityPreferenceKeyName);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Setting player camera sensitivity preference to default value: 0.5 ");
            localCameraSensitivity = 0.5f;
            PlayerPrefs.SetFloat(playerSensitivityPreferenceKeyName, 0.5f);
        }
        try
        {
            smb_controls = PlayerPrefs.GetInt(playerSMBControlPreferenceKey);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Setting player smb preference to default value: 0 ");
            smb_controls = 0;
            PlayerPrefs.SetInt(playerSMBControlPreferenceKey, 0);
        }

        //Make sure all panels are active
        titleCard.gameObject.SetActive(true);
        mainMenuCard.gameObject.SetActive(true);
        loadingCard.gameObject.SetActive(true);
        settingsCard.gameObject.SetActive(false);

       

        audio_volume = localAudioVolume;
        camera_sensitivity = localCameraSensitivity;
        //preset sliders
        
        controller.sendNavigationEvents = false;
        audio_player.time = 422;
    }
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetButtonDown("start") || Input.GetButtonDown("Jump")) && !audio_player.isPlaying)
        {
            Select();
            GotoMainMenu();
            controller.sendNavigationEvents = true;
            controller.firstSelectedGameObject.GetComponent<Selectable>().Select();
        }
        if (Input.GetButtonDown("Jump"))
            Select();
        if (async != null)
	    {
	        Debug.LogWarning("Percent Load: " + async.progress);
	    }
	}

    public void GotoMainMenu()
    {
        audio_player.volume = 0;
        audio_player.Play();
        InvokeRepeating("FadeMusicVolume", 0, 0.03f);
        titleCard.gameObject.SetActive(false);
        mainMenuCard.gameObject.SetActive(true);
        
    }

    public void StartGame()
    {
        StartLoading();
    }

    public void Select()
    {
        SFX.PlayOneShot(select);
    }

    public void Move()
    {
        SFX.PlayOneShot(move);
    }

    

    public void StartLoading()
    {
        loadingCard.gameObject.SetActive(true);
        titleCard.gameObject.SetActive(true);
        mainMenuCard.gameObject.SetActive(true);
        StartCoroutine("load");
    }

    IEnumerator load()
    {
        Debug.LogWarning("ASYNC LOAD STARTED - " +
           "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");
        async = SceneManager.LoadSceneAsync(levelName);
        async.allowSceneActivation = true;
        yield return async;
    }

    public void ActivateScene()
    {
        async.allowSceneActivation = true;
    }

    public void SetMusic(UnityEngine.Object set)
    {
        float volume = ((Slider) set).value;
        audio_volume = volume;
        audio_player.volume = volume;
        PlayerPrefs.SetFloat(playerMusicVolumePreferenceKeyName, volume);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetAxisSensitivity(Slider s)
    {
        float sensitivity = s.value;
        PlayerPrefs.SetFloat(playerSensitivityPreferenceKeyName, sensitivity);
        sensitivity_text.text =(sensitivity*5).ToString("0");
    }

    private void FadeMusicVolume()
    {
        if (audio_player.volume < audio_volume)
            audio_player.volume += 0.01f;
        else
        {
            CancelInvoke("FadeMusicVolume");
        }
    }

    public void SetSliderValues()
    {
        GameObject.Find("SSlider").GetComponent<Slider>().value = camera_sensitivity;
        GameObject.Find("MVSlider").GetComponent<Slider>().value = audio_volume;
        bool smb = false;
        if (smb_controls == 1)
            smb = true;
        GameObject.Find("SMBControl").GetComponent<Toggle>().isOn = smb;
    }

    public void SetSMBPrefs(Toggle s)
    {
        int smbpref = 0;
        if (s.isOn)
            smbpref = 1;
        PlayerPrefs.SetInt(playerSMBControlPreferenceKey, smbpref);
    }

    public void LoadLevel(String level)
    {
        levelName = level;
        StartCoroutine("load");
    }

}
