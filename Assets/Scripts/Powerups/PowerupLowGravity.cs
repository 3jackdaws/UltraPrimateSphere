using UnityEngine;
using System.Collections;

public class PowerupLowGravity : MonoBehaviour, IPowerUp {

    public int respawn_time;
    public int low_gravity_time;
    private AudioSource player_object;
    public AudioClip pick_up_sound;
    public AudioClip use_sound;

    private Vector3 prev_gravity;
    // Use this for initialization
    void Start ()
    {
        prev_gravity = Physics.gravity;
        player_object = GameObject.Find("Player").GetComponentsInChildren<AudioSource>()[3];
    }
	
	// Update is called once per frame
	void Update () {
        transform.transform.rotation = Quaternion.AngleAxis(0.5f, Vector3.up) * transform.rotation;
    }

    public void OnTriggerEnter(Collider cevent)
    {
        cevent.gameObject.GetComponentInChildren<QuadMovement>().AddPowerUp(this);
        //GetComponent<Renderer>().enabled = false;
        //GetComponentInChildren<Renderer>().enabled = false;
        //this.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
        gameObject.GetComponent<Collider>().enabled = false;
        Renderer[] all = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in all)
        {
            r.enabled = false;
        }
        player_object.volume = 0.9f;
        player_object.pitch = 0.7f;
        player_object.PlayOneShot(pick_up_sound);
        StartCoroutine("Respawn");



    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawn_time);
        Renderer[] all = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in all)
        {
            r.enabled = true;
        }
        gameObject.GetComponent<Collider>().enabled = true;
    }

    public void UsePowerUp(GameObject gObject, IPowerUp powerup)
    {
        player_object.PlayOneShot(use_sound);
        Physics.gravity = Vector3.down * 8;
        StartCoroutine("ResetGravity");
        gObject.GetComponent<QuadMovement>().AddPowerUp(null);
    }

    public string Name()
    {
        return "Anti-Gravity";
    }

    IEnumerator ResetGravity()
    {
        yield return new WaitForSeconds(low_gravity_time);
        Physics.gravity = prev_gravity;
    }
}
