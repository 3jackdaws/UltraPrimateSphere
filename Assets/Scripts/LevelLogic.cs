using UnityEngine;
using System.Collections;
using Animation = UnityEngine.Animation;

public class LevelLogic : MonoBehaviour
{
    public GameObject spawner;
    //public Animation spawnAnimator;
    private Vector3 spawnerInitialLocation;
    private GameObject player;
    public AudioSource standard_output;
    public AudioClip spawnSound;
	// Use this for initialization
	void Start ()
	{
	    spawnerInitialLocation = spawner.transform.position;
        player = GameObject.Find("Player");
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("select"))
	    {
	        ResetLevel();
	    }
        if(player.transform.position.y < -20f)
            ResetLevel();
	}

    private void ResetLevel()
    {
        standard_output.pitch = 1;
        standard_output.PlayOneShot(spawnSound);
        spawner.transform.position = spawnerInitialLocation;
        player.transform.position = spawnerInitialLocation + Vector3.up*0.2f;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        spawner.GetComponent<UnityEngine.Animation>().Play();
        player.GetComponentInChildren<QuadMovement>().SetFacingVector(Vector3.right);
    }
}
