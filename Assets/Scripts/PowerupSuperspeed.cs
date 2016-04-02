using System;
using UnityEngine;
using System.Collections;

public class PowerupSuperspeed : MonoBehaviour, IPowerUp {

	// Use this for initialization
    public int super_speed_force;
    public int respawn_time;
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.transform.rotation = Quaternion.AngleAxis(0.5f, Vector3.up) * transform.rotation;
	}

    void OnCollisionEnter(Collision cevent)
    {
        cevent.gameObject.GetComponentInChildren<QuadMovement>().AddPowerUp(this);
        GetComponent<Renderer>().enabled = false;
        this.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
        StartCoroutine("Respawn");
        //GetComponent<Renderer>().enabled = true;

    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(6f);
        GetComponent<Renderer>().enabled = true;
        gameObject.GetComponent<Rigidbody>().detectCollisions = true;
    }

    public void UsePowerUp(GameObject gObject, IPowerUp powerUp)
    {
        gObject.GetComponentInParent<Rigidbody>().AddForce(gObject.GetComponent<QuadMovement>().GetFacing2D() * super_speed_force, ForceMode.Impulse);
        gObject.GetComponent<QuadMovement>().AddPowerUp(null);
    }
}
