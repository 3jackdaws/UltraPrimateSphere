﻿using UnityEngine;
using System.Collections;
using Animation = UnityEngine.Animation;

public class LevelLogic : MonoBehaviour
{
    
    private Vector3 initialLocation;
    private GameObject player;
    public AudioSource standard_output;
    public AudioClip spawnSound;
    public int reset_level;
    public ParticleSystem spawn_effect;
    private bool resetting = false;
	// Use this for initialization
	void Start ()
	{
	    
        player = GameObject.Find("Player");
        initialLocation = player.transform.position;
        ResetLevel();
        //player.GetComponentInChildren<QuadMovement>().SetInitialPos(initialLocation);
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("select"))
	    {
	        ResetLevel();
	    }
        if(player.transform.position.y < reset_level)
            ResetLevel();
	    if (Input.GetKeyDown("escape"))
	    {
            Cursor.lockState = CursorLockMode.None;
	        Cursor.visible = true;
	    }
        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void ResetLevel()
    {
        
        if (!resetting)
        {
            resetting = true;
            standard_output.pitch = 1;
            standard_output.PlayOneShot(spawnSound);
            Invoke("ResetPlayer", 2);
        }
            
    }

    void ResetPlayer()
    {
        player.transform.position = initialLocation;
        ParticleSystem p = (ParticleSystem) Instantiate(spawn_effect);
        p.loop = false;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponentInChildren<QuadMovement>().SetFacingVector(Vector3.right);
        resetting = false;
    }
}
