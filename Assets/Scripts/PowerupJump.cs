using System;
using UnityEngine;
using System.Collections;

public class PowerupJump : MonoBehaviour, IPowerUp
{

    // Use this for initialization
    public int jump_force;
    public int respawn_time;
    public AudioSource player_object;
    public AudioClip pick_up_sound;
    public AudioClip use_sound;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.transform.rotation = Quaternion.AngleAxis(0.5f, Vector3.up) * transform.rotation;
    }

    void OnCollisionEnter(Collision cevent)
    {
        cevent.gameObject.GetComponentInChildren<QuadMovement>().AddPowerUp(this);
        GetComponent<Renderer>().enabled = false;
        this.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
        player_object.volume = 0.9f;
        player_object.pitch = 0.7f;
        player_object.PlayOneShot(pick_up_sound);
        StartCoroutine("Respawn");
        //GetComponent<Renderer>().enabled = true;

    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawn_time);
        GetComponent<Renderer>().enabled = true;
        gameObject.GetComponent<Rigidbody>().detectCollisions = true;
    }

    public void UsePowerUp(GameObject gObject, IPowerUp powerUp)
    {
        gObject.GetComponentInParent<Rigidbody>().AddForce(Vector3.up * jump_force, ForceMode.Impulse);
        player_object.volume = 0.9f;
        player_object.pitch = 0.7f;
        player_object.PlayOneShot(use_sound);
        gObject.GetComponent<QuadMovement>().AddPowerUp(null);
    }
}
