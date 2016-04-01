using System;
using UnityEngine;
using System.Collections;
using System.Threading;

public class Projectile : MonoBehaviour
{
    public Rigidbody projectile;
    public Transform Spawnpoint;
    public int hi;
    public GameObject vector_parent;
    public int proj_force;

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Rigidbody clone = (Rigidbody)Instantiate(projectile, Spawnpoint.position, projectile.rotation);

            clone.AddForce(vector_parent.GetComponent<QuadMovement>().GetFacing()*proj_force, ForceMode.Impulse);
            
          

            clone.Sleep();
        }
    }

    
}