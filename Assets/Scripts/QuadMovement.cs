using UnityEngine;
using System.Collections;
using System.Timers;
using UnityEditor.VersionControl;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Timer = System.Threading.Timer;

public class QuadMovement : MonoBehaviour {

    private Rigidbody rb;
    public float speed;
    public float gravity;
    private Vector3 facing;
    public float jump_force;
    private GameObject gui;
    private Collider mesh;
    private UnityEngine.UI.Text debug;
    public bool DebugText = true;
    private bool canJump = true;
	public int air_movement_speed = 10;
    private float distToGround;
	private Vector3 facing2D;
	private Vector3 initialPosition;

    void Start()
    {
		rb = transform.parent.GetComponent<Rigidbody>();
        gui = GameObject.Find("DebugFacing");
        facing = facing2D = new Vector3(1, 0, 0);
        Physics.gravity = new Vector3(0, -gravity, 0);
        mesh = GetComponent<Collider>();
        rb.maxAngularVelocity = 100;
        debug = gui.GetComponent<Text>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
		debug.text = "Test";
		initialPosition = rb.transform.position;
    }

    private bool isGrounded(){
        return Physics.Raycast(transform.transform.position, -Vector3.up, distToGround+0.05f);

    }

	void Update()
    {
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;
		if (Input.GetKeyDown (KeyCode.R)) {
			Reset ();
		}
	    if (Input.GetAxis("Fire1") == 1)
	    {
	        Shoot();
	    }
    }

	void Reset()
	{
		rb.transform.position = initialPosition;
	}
  

    void FixedUpdate()
    {
        float force = speed * rb.mass;
        float strafeX = Input.GetAxis("Vertical");
        float strafeY = Input.GetAxis("Horizontal");
        float mouseX = 0;
        float mouseY = 0;
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = 0.1f*Input.GetAxis("Mouse Y");
        }
        else
        {
            mouseX = Input.GetAxis("CameraHorizontal");
            mouseY = 0.05f * Input.GetAxis("CameraVertical");
        }
        if (Input.GetAxis("Jump") > 0)
        {

            if (isGrounded())
            {
                rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
            }
                
        }
		Vector3 movementVector = new Vector3 (strafeX, 0, strafeY);

		facing2D = Quaternion.Euler (0, 5 * mouseX, 0) * facing2D;
        facing = Quaternion.Euler(0, 5 * mouseX, 0) * facing;
        Vector3 torqueVector = Quaternion.AngleAxis(-90, Vector3.down) * facing2D;
        facing = facing + Vector3.up*mouseY;
        facing.Normalize();

		rb.AddTorque(torqueVector*strafeX*force - facing2D*strafeY*force);
		rb.AddForce (torqueVector*strafeY*air_movement_speed + facing2D*strafeX*air_movement_speed);
        

        //this.transform.Rotate(torqueVector, 15*forward*speed);


        if (DebugText)
            debug.text = mouseX.ToString() + "     " + Input.GetAxis("Jump");
        // else
        //debug.text = "";
    }

    public Vector3 GetFacing()
    {
        return facing;
    }

    private void Shoot()
    {
        
    }
}


