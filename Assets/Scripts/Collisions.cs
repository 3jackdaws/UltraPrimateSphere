using UnityEngine;
using System.Collections;

public class Collisions : MonoBehaviour
{
    public AudioClip hard_collide;
    public AudioClip soft_collide;
    public AudioClip rolling;
    public AudioSource source;
    public AudioSource hitsource;
    public Rigidbody track;
    public GameObject innerSphere;
    private float prev_frame_pos;
    public float distance_check = 125;

    public int hitboost;
	// Use this for initialization
	void Start () {
	     source.loop = true;
        source.clip = rolling;
        source.Play();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    prev_frame_pos = track.position.y;
	    if (!innerSphere.GetComponent<QuadMovement>().isRolling())
	        source.Pause();
	    else
	    {
	        if(!source.isPlaying)
                source.UnPause();
            source.volume = track.velocity.magnitude / 100;
            source.pitch = 0.6f + track.velocity.magnitude / 100;
        }
            
	    
	}

    void OnCollisionEnter(Collision cevent)
    {
        if (cevent.impulse.y > distance_check)
        {
            hitsource.volume = cevent.relativeVelocity.magnitude/50;
            hitsource.pitch = Random.value/20 + 0.4f;
            hitsource.PlayOneShot(hard_collide);
        }
        
    }
}
