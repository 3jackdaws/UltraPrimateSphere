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
    }

    public bool isGrounded(){
        return Physics.Raycast(transform.transform.position, -Vector3.up, distToGround+0.05f);
    }

    public bool isRolling()
    {
        return Physics.Raycast(transform.transform.position, -Vector3.up, distToGround + 0.2f);
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

    private void Jump()
    {
        if (canJump && isGrounded())
        {
            canJump = false;

            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += (Object, args) => { canJump = true; };
            aTimer.Interval = 100;
            aTimer.AutoReset = false;
            aTimer.Start();
            rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
            boostOut.volume = 0.8f;
            boostOut.pitch = 1f;
            boostOut.PlayOneShot(jump_sound);
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
}


