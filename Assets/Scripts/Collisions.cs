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
    private bool isRolling;

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
        if(isRolling)
            source.volume = track.velocity.magnitude / 25;
    }

    void OnCollisionEnter(Collision cevent)
    {
        movement_script.SetJumpVector(cevent.contacts[0].normal.normalized);
        movement_script.OnGround = true;
        Roll();
        if (cevent.impulse.magnitude > distance_check)
        {
            hitsource.volume = cevent.relativeVelocity.magnitude/50;
            hitsource.pitch = Random.value/20 + 0.4f;
            hitsource.PlayOneShot(hard_collide);
        }
        
        //movement_script.SetJumpVector(cevent.contacts[0].normal.normalized);
        
        
    }

    void OnCollisionStay(Collision col)
    {
        Roll();
        movement_script.SetJumpVector(col.contacts[0].normal.normalized);
        movement_script.OnGround = true;
    }

    void OnCollisionExit()
    {
        movement_script.OnGround = false;
        isRolling = false;
        source.volume = 0;
    }

    public void Roll()
    {
        isRolling = true;
    }

    void CheckGrounded()
    {
        movement_script.IfGrounded();
    }
}
