using System;
using UnityEngine;
using System.Collections;

public class PowerupJump : MonoBehaviour, IPowerUp
{

    // Use this for initialization
    public int jump_force;
    public int respawn_time;
    private AudioSource player_object;
    public AudioClip pick_up_sound;
    public AudioClip use_sound;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.transform.rotation = Quaternion.AngleAxis(0.5f, Vector3.up) * transform.rotation;
        player_object = GameObject.Find("Player").GetComponentsInChildren<AudioSource>()[3];
    }

    void OnTriggerEnter(Collider cevent)
    {
        cevent.gameObject.GetComponentInChildren<QuadMovement>().AddPowerUp(this);
        //GetComponent<Renderer>().enabled = false;
        //GetComponentInChildren<Renderer>().enabled = false;
        //this.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
        gameObject.GetComponent<Collider>().enabled = false;
        Renderer[] all = GetComponentsInChildren<Renderer>();
        foreach(Renderer r in all)
        {
            r.enabled = false;
        }
        player_object.volume = 0.9f;
        player_object.pitch = 0.7f;
        player_object.PlayOneShot(pick_up_sound);
        StartCoroutine("Respawn");
        //GetComponent<Renderer>().enabled = true;

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

    public void UsePowerUp(GameObject gObject, IPowerUp powerUp)
    {
        gObject.GetComponentInParent<Rigidbody>().AddForce(Vector3.up * jump_force, ForceMode.Impulse);
        player_object.volume = 0.9f;
        player_object.pitch = 0.7f;
        player_object.PlayOneShot(use_sound);
        gObject.GetComponent<QuadMovement>().AddPowerUp(null);
    }

    public string Name()
    {
        return "Super Jump";
    }
}
