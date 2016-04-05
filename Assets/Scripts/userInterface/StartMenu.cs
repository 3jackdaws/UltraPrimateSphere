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
	// Use this for initialization
	void Start ()
	{
	    title.enabled = true;
	    MainMenu.enabled = false;
        audio_player.time = 423;
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
        //AsyncOperation scene = SceneManager.LoadSceneAsync(1);
        //scene.allowSceneActivation = true;
        //while (!scene.isDone)
        //{
        //    Console.WriteLine("Progress: {0}", scene.progress);
        //    yield;
        //    ;
        //}
    }

    public void Select()
    {
        SFX.PlayOneShot(select);
    }

    public void Move()
    {
        SFX.PlayOneShot(move);
    }


}
