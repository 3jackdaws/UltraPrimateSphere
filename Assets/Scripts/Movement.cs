using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float gravity;
    private Vector3 facing;
    private GameObject gui;
    private Collider mesh;
    private UnityEngine.UI.Text debug;
    public bool DebugText = true;
	
	void Start ()
	{
	    rb = GetComponent<Rigidbody>();
        gui = GameObject.Find("DebugFacing");
        facing = new Vector3(1,0,0);
        Physics.gravity = new Vector3(0,-gravity, 0);
	    mesh = GetComponent<Collider>();
	    rb.maxAngularVelocity = 100;
        debug = gui.GetComponent<Text>();
    }
	
	void FixedUpdate ()
	{
	    float force = speed*rb.mass;
	    float forward = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");
	    facing = Quaternion.Euler(0, 5*turn, 0) * facing;
        //if(mesh.)
        //rb.AddForce(facing * forward * force);
	    Vector3 torqueVector = Quaternion.AngleAxis(-90, Vector3.down)*facing;
        //ForceMode
	    rb.AddTorque(torqueVector * forward * force);
        //rb.AddTorque(Vector3.forward * forward * speed);

        //this.transform.Rotate(torqueVector, 15*forward*speed);


	    if (DebugText)
	        debug.text = "V: " + forward + " H: " + turn + " Facing: " + facing + "\nApplied torque: " +
	                     torqueVector.magnitude*forward*force + " Rotation Velocity: " + rb.angularVelocity.magnitude;
	    else
	        debug.text = "";
	}

    public Vector3 GetFacing()
    {
        return facing;
    }
}
