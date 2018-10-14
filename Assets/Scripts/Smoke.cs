using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public Transform player;

    int frameCount;
	// Use this for initialization
	void Start ()
    {
        frameCount = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.timeScale == 1.0f)
        {
            frameCount++;

            if (frameCount == 60)
            {
                Destroy(this.gameObject);
            }

            transform.LookAt(player);
        }
    }
}
