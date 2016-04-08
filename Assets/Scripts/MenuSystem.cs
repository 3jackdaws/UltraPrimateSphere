using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    public Canvas startMenu;
    public EventSystem cont;
    public Scene mainMenu;
	void Start ()
	{
	    startMenu.enabled = false;
	    cont.sendNavigationEvents = false;
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
            cont.sendNavigationEvents = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
        SceneManager.LoadScene("MainMenu");
    }
}
