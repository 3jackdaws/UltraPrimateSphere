using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestAxis : MonoBehaviour {
    public Text tout;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    tout.text = Input.GetAxisRaw("Horizontal").ToString() + "\n" + Input.GetAxisRaw("Vertical").ToString();
	}
}
