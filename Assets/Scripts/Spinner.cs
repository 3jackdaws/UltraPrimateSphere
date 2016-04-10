using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour
{
    public float speed;
    public int collider_force;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //transform.transform.rotation = Quaternion.AngleAxis(speed, Vector3.up) * transform.rotation;
    }

    void OnCollisionEnter(Collision cevent)
    {
       cevent.collider.attachedRigidbody.AddForce((Vector3.up) * collider_force, ForceMode.Impulse);
    }

    //void OnCollisionStay(Collision cevent)
    //{
    //    cevent.collider.attachedRigidbody.AddForce(cevent.contacts[0].normal * collider_force, ForceMode.Impulse);
    //}
}
