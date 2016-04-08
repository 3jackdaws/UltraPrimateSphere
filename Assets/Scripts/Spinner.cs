using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour
{
    public int speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.transform.rotation = Quaternion.AngleAxis(speed, Vector3.up) * transform.rotation;
    }
}
