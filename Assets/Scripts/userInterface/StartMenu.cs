using System;
using UnityEngine;
using System.Collections;
using System.Xml.Schema;
using UnityEngine.SceneManagement;

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
    // Use this for initialization
    void Start ()
	{
	    title.enabled = true;
	    MainMenu.enabled = false;
        loadingScreen.enabled = false;
        audio_player.time = 320;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("start") && title.enabled)
        {
            Select();
            GotoMainMenu();
        }
        if (Input.GetButtonDown("Jump"))
            Select();
        if (audio_player.isPlaying && audio_player.volume < 1)
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
        MainMenu.enabled = true;
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


}
