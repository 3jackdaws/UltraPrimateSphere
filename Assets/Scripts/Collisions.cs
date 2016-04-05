using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Collisions : MonoBehaviour
{
    public AudioClip hard_collide;
    public AudioClip soft_collide;
    public AudioClip rolling;
    public AudioSource source;
    public AudioSource hitsource;
    public Rigidbody track;
    public GameObject innerSphere;
    private QuadMovement movement_script;
    public float distance_check = 125;

    public int hitboost;
	// Use this for initialization
	void Start () {
	     source.loop = true;
        source.clip = rolling;
        source.Play();
	    movement_script = innerSphere.GetComponent<QuadMovement>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
        source.pitch = 0.6f + track.velocity.magnitude / 100;
        source.volume = track.velocity.magnitude / 50;
    }

    void OnCollisionEnter(Collision cevent)
    {
        movement_script.OnGround = true;
        Roll();
        if (cevent.impulse.magnitude > distance_check)
        {
            hitsource.volume = cevent.relativeVelocity.magnitude/50;
            hitsource.pitch = Random.value/20 + 0.4f;
            hitsource.PlayOneShot(hard_collide);
        }
        
        
    }

    void OnCollisionExit()
    {
        movement_script.OnGround = false;
        source.Pause();
    }

    void Roll()
    {
        if (!source.isPlaying)
            source.UnPause();
    }
}
