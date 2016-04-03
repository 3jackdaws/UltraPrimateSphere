using System;
using UnityEngine;
using System.Collections;
using System.Timers;
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
    private bool canBoost = true;
	public int air_movement_speed = 10;
    private float distToGround;
	private Vector3 facing2D;
	private Vector3 initialPosition;
    public int boost_force ;
    public ParticleSystem boost_effect;
    public AudioSource boostOut;
    public AudioClip boost_sound;
    public float boost_recharge;
    public AudioClip jump_sound;
    private IPowerUp heldPowerup;
    private Vector3 jumpVector;

    void Start()
    {
		rb = transform.parent.GetComponent<Rigidbody>();
        gui = GameObject.Find("DebugFacing");
        facing = facing2D = new Vector3(1, 0, 0);
        Physics.gravity = new Vector3(0, -gravity, 0);
        mesh = GetComponent<Collider>();
        rb.maxAngularVelocity = 100;
        //debug = gui.GetComponent<Text>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
		//debug.text = "Test";
		initialPosition = rb.transform.position;
        InvokeRepeating("IfGrounded", 2, 0.3F);
    }

    public bool isGrounded()
    {
       // RaycastHit hitInfo = new RaycastHit();
        return Physics.Raycast(transform.transform.position, Vector3.down, distToGround + 0.03f);
        //jumpVector = Vector3.zero;
        //if (hitInfo.distance > 0)
        //{
        //    //if(transform.transform.position.y > initialPosition.y+5)
        //    //    Console.WriteLine("Oh hai");
        //    Vector3 v = transform.transform.position - hitInfo.collider.ClosestPointOnBounds(transform.transform.position);
        //    if (v.magnitude < distToGround + 0.1f)
        //        return true;
        //    return false;
        //} 
        //else
        //{
        //    return false;
        //}
        //Physics.Raycast(transform.transform.position, Vector3.down, distToGround+0.02f);
    }

    private void IfGrounded()
    {
        if (isGrounded())
            canJump = true;
    }

    public bool isRolling()
    {
        return Physics.Raycast(transform.transform.position, -Vector3.up, distToGround + 0.5f);
    }

	void Update()
    {
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;
		//if (Input.GetButtonDown("select")) {
		//	Reset ();
		//}
	    if (Input.GetButtonDown("Boost"))
	    {
	        Boost();
	    }
        if (Input.GetAxis("Jump") > 0)
        {
            Jump();
        }
	    if (Input.GetAxis("PowerUp") > 0)
	    {
            if(heldPowerup != null)
	            heldPowerup.UsePowerUp(this.gameObject, heldPowerup);
	    }
	    
    }

	void Reset()
	{
		rb.transform.position = initialPosition;
	    rb.angularVelocity = Vector3.zero;
	    rb.velocity = Vector3.zero;
	}

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        string text = "NULL";
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        RaycastHit hitinfo = new RaycastHit();
        Physics.Raycast(new Ray(transform.transform.position, Vector3.down*100), out hitinfo);
        Debug.DrawRay(transform.transform.position, Vector3.down * 100);
        if (!hitinfo.Equals(null))
            text = hitinfo.distance.ToString();
        GUI.Label(rect, text, style);
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
        
		Vector3 movementVector = new Vector3 (strafeX, 0, strafeY);

		facing2D = Quaternion.Euler (0, 5 * mouseX, 0) * facing2D;
        facing = Quaternion.Euler(0, 5 * mouseX, 0) * facing;
        Vector3 torqueVector = Quaternion.AngleAxis(-90, Vector3.down) * facing2D;
        facing = facing + Vector3.up*mouseY;
        facing.Normalize();

		rb.AddTorque(torqueVector*strafeX*force - facing2D*strafeY*force);
		rb.AddForce (torqueVector*strafeY*air_movement_speed + facing2D*strafeX*air_movement_speed);
        

        //this.transform.Rotate(torqueVector, 15*forward*speed);


        //
        // else
        //debug.text = "";
    }

    public Vector3 GetFacing()
    {
        return facing;
    }

    public Vector3 GetFacing2D()
    {
        return facing2D;
    }

    private void Boost()
    {
        if (canBoost)
        {
            canBoost = false;
            boostOut.volume = 0.8f;
            boostOut.pitch = 1f;
            boostOut.PlayOneShot(boost_sound);
            ParticleSystem boost = (ParticleSystem)Instantiate(boost_effect, transform.position, boost_effect.transform.rotation);
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += (Object, args) => { canBoost = true;};
            aTimer.Interval = boost_recharge*1000;
            aTimer.AutoReset = false;
            aTimer.Start();
            rb.AddForce(Vector3.up * boost_force, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collider other)
    {
        canJump = true;
    }

    void OnCollisionExit(Collider other)
    {
        canJump = false;
    }

    private void Jump()
    {
        if (canJump)
        {
            canJump = false;

            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += (Object, args) => { canJump = true; };
            aTimer.Interval = 200;
            aTimer.AutoReset = false;
            //aTimer.Start();
            //if (jumpVector.magnitude > 0.1)
            //{
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
                boostOut.volume = 0.8f;
                boostOut.pitch = 1f;
                boostOut.PlayOneShot(jump_sound);
            //}
            
        }
    }

    public void AddPowerUp(IPowerUp pu)
    {
        heldPowerup = pu;
    }


    private void ResetBoolean(object Object, ElapsedEventArgs args)
    {
        canJump = true;
    }

    public void SetJump(bool val)
    {
        canJump = val;
    }

    public void SetJumpVector(Vector3 vect)
    {
        jumpVector = vect;
    }
}


