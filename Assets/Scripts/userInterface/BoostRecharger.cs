using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoostRecharger : MonoBehaviour
{

    public GameObject boostController;
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update ()
	{
	    GetComponent<Slider>().value = boostController.GetComponent<QuadMovement>().GetBoostValue();
	}
}
