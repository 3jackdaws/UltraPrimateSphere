using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UICard : MonoBehaviour
{

    public Selectable firstObjectSelected;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ActivateCard()
    {
        firstObjectSelected.Select();
    }
}
