using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SMBControls : MonoBehaviour {

    private Rigidbody rb;
    public float speed;
    public float gravity;
    private Vector3 facing;
    public float jump_force;
    public bool DebugText = true;
    private RollerballInput _rollerballInput;
    private Collisions ccontroller;

    private bool canJump = true;
    private bool onGround = true;

    public bool OnGround
    {
        set { onGround = value; }
        get { return onGround; }
    }
    public int air_movement_speed = 10;
    private float distToGround;
    private Vector3 facing2D;
    private Vector3 initialPosition;
    public int boost_force;
    public ParticleSystem boost_effect;
    public AudioSource boostOut;
    public AudioClip boost_sound;
    public float boost_recharge;
    public AudioClip jump_sound;
    private IPowerUp heldPowerup;
    public float boost_value;
    public Text PowerUpUIText;
    private Vector3 jumpVector;
    // Use this for initialization
    void Start () {
        rb = transform.parent.GetComponent<Rigidbody>();
        facing = facing2D = new Vector3(1, 0, 0);
        rb.maxAngularVelocity = 100;
        ccontroller = GetComponentInParent<Collisions>();
        //distToGround = GetComponent<Collider>().bounds.extents.y;
        InvokeRepeating("SetPowerUpUI", 2, 0.2f);
        InvokeRepeating("RechargeBoost", 1, 0.05f);

        _rollerballInput = new RollerballInput();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale > 0)
        {
            GetRollerballInput();
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
                if (heldPowerup != null)
                    heldPowerup.UsePowerUp(this.gameObject, heldPowerup);
            }

        }
    }

    void FixedUpdate()
    {
        float force = speed * rb.mass;
        facing = Quaternion.Euler(0, 5 * _rollerballInput.xMove, 0) * facing;
        facing2D = new Vector3(facing.x, 0, facing.z);
        Vector3 torqueVector = Quaternion.AngleAxis(-90, Vector3.down) * facing2D;
        facing += Vector3.up * _rollerballInput.yCamera;
        facing.Normalize();

        rb.AddTorque(torqueVector * _rollerballInput.xMove * force - facing2D * _rollerballInput.yMove * force);
        rb.AddForce(torqueVector * _rollerballInput.yMove * air_movement_speed + facing2D * _rollerballInput.xMove * air_movement_speed);
    }

    void OnTriggeEnter()
    {
        ccontroller.Roll();
    }

    void OnTriggerStay()
    {
        ccontroller.Roll();
    }
    void GetRollerballInput()
    {
        _rollerballInput.xMove = Input.GetAxis("Vertical");
        _rollerballInput.yMove = Input.GetAxis("Horizontal");
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
        if (boost_value >= 1)
        {
            boost_value = 0;
            boostOut.volume = 0.8f;
            boostOut.pitch = 1f;
            boostOut.PlayOneShot(boost_sound);
            ParticleSystem boost = (ParticleSystem)Instantiate(boost_effect, transform.position, boost_effect.transform.rotation);
            rb.AddForce(Vector3.up * boost_force, ForceMode.Impulse);
        }
    }


    private void Jump()
    {
        if (canJump && onGround)
        {
            canJump = false;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(jumpVector * jump_force, ForceMode.Impulse);
            boostOut.volume = 0.8f;
            boostOut.pitch = 1f;
            boostOut.PlayOneShot(jump_sound);
            Invoke("ResetJump", 0.1f);
        }
    }

    public void AddPowerUp(IPowerUp pu)
    {
        heldPowerup = pu;
    }

    public float GetBoostValue()
    {
        return boost_value;
    }

    public void SetFacingVector(Vector3 v)
    {
        facing = v;
    }

    public void SetInitialPos(Vector3 v)
    {
        initialPosition = v;
    }

    public String GetPowerUpName()
    {
        if (heldPowerup != null)
            return heldPowerup.Name();
        return "";
    }

    public void SetPowerUpUI()
    {
        PowerUpUIText.text = GetPowerUpName();
    }

    private void RechargeBoost()
    {
        if (boost_value < 1)
        {
            boost_value += 0.01f;
        }
    }

    private void ResetJump()
    {
        canJump = true;
    }

    public bool isGrounded()
    {
        return Physics.Raycast(transform.transform.position, Vector3.down, distToGround + 0.03f);
    }

    public void IfGrounded()
    {
        if (!canJump && isGrounded())
            canJump = true;
    }

    public bool isRolling()
    {
        return canJump;
        
    }

    void Reset()
    {
        rb.transform.position = initialPosition;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    public void SetJumpVector(Vector3 jump)
    {
        jumpVector = jump;
    }
}
