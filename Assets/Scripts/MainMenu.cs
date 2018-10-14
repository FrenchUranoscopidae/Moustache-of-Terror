using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Animator animator;
    public GameObject background;
	// Use this for initialization
	void Start ()
    {
        animator = background.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("joystick button 7"))
        {
            animator.SetBool("play", true);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("New Animation") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.65f)
        {
            SceneManager.LoadScene("Cinematic");
        }
	}
}
