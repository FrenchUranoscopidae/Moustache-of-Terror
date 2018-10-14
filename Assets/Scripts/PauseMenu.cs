using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        this.gameObject.GetComponent<Canvas>().enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown("joystick button 7"))
        {
            ToggleMenu();
        }
	}

    void ToggleMenu()
    {
        if (this.gameObject.GetComponent<Canvas>().enabled)
        {
            this.gameObject.GetComponent<Canvas>().enabled = false;
            Time.timeScale = 1.0f;
        }
        else
        {
            this.gameObject.GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0.0f;
        }
        Debug.Log("Done");
    }
}
