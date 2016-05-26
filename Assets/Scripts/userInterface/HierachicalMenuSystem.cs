using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HierachicalMenuSystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ActivateChildMenu(Object parent)
    {
        RectTransform firstChild = ((GameObject) parent).GetComponentInChildren<RectTransform>(true);
        firstChild.gameObject.SetActive(true);
        Selectable button = ((GameObject)parent).GetComponentInChildren<Button>();
        button.Select();
    }
}
