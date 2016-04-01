using UnityEngine;
using System.Collections;


public class Origin : MonoBehaviour
{
    public GameObject scriptParent;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.position = scriptParent.transform.position + scriptParent.GetComponent<QuadMovement>().GetFacing();
	}
}
