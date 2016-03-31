using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{
    private GameObject player;
    private GameObject pmove;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        pmove = GameObject.Find("Movement");
    }
	
	// Update is called once per frame
	void Update ()
	{
        transform.position = player.transform.position - player.GetComponent<Movement>().GetFacing()*3 + new Vector3(0, 0.6f, 0);
        transform.LookAt(player.transform);
	    //transform.transform.position = player.transform.transform.position;
	}
}
