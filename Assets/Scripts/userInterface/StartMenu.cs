using System;
using UnityEngine;
using System.Collections;
using System.Xml.Schema;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class StartMenu : MonoBehaviour
{
    public AudioSource audio_player;
    public AudioSource SFX;
    public AudioClip select;
    public AudioClip move;
    public Canvas title;
    public Canvas MainMenu;
    public Scene level1;
    public string levelName;
    public Canvas loadingScreen;
    AsyncOperation async;
    public EventSystem controller;
    private float audio_volume;

    public Text sensitivity_text;
    
    // Use this for initialization
    void Start ()
	{
        title.gameObject.SetActive(true);
	    title.enabled = true;
        audio_volume = 1;
       
	    MainMenu.gameObject.SetActive(false);
        //MainMenu.GetComponents<Component>()[2].gameObject.SetActive(false);
        controller.sendNavigationEvents = false;
        loadingScreen.enabled = false;
        audio_player.time = 320 ;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("start") || Input.GetButtonDown("Jump") && title.enabled)
        {
            Select();
            GotoMainMenu();
            controller.sendNavigationEvents = true;
            controller.firstSelectedGameObject.GetComponent<Selectable>().Select();
        }
        if (Input.GetButtonDown("Jump"))
            Select();
        if (audio_player.isPlaying && audio_player.volume < audio_volume)
            audio_player.volume += 0.01f;
        if (async != null)
	    {
	        Debug.LogWarning("Percent Load: " + async.progress);
	    }
	}

    public void GotoMainMenu()
    {
        audio_player.volume = 0;
        audio_player.Play();
        title.enabled = false;
        MainMenu.gameObject.SetActive(true);
        //MainMenu.
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
        loadingScreen.enabled = true;
        title.enabled = false;
        MainMenu.enabled = false;
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
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetAxisSensitivity(Slider s)
    {
        float sensitivity = s.value;
        GlobalInput.SetCameraSensitivity(sensitivity);
        sensitivity_text.text =(sensitivity*5).ToString("0");
    }


}
