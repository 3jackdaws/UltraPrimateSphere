using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    public Canvas startMenu;
    public Selectable[] menuChildren;
    public int selected;
    public EventSystem cont;
    public Scene mainMenu;
	void Start ()
	{
	    menuChildren = startMenu.GetComponentsInChildren<Selectable>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetButtonDown("start"))
	    {
	       
	            MenuToggle();
	    }
	   
    }

    public void MenuToggle()
    {
        if (startMenu.enabled == false)
        {
            Time.timeScale = 0;
            startMenu.enabled = true;
            selected = 0;
            cont.sendNavigationEvents = true;

        }
        else
        {
            startMenu.enabled = false;
            Time.timeScale = 1;
            Input.ResetInputAxes();
            cont.sendNavigationEvents = false;
        }
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void GotoMainMenu()
    {
        SceneManager.SetActiveScene(mainMenu);
    }
}
