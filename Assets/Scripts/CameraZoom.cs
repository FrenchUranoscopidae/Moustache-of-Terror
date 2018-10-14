using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraZoom : MonoBehaviour {
	public GameObject light;
    Color lerpedColor = Color.white;
    Light lighting;
    float tone;
	// Use this for initialization
	void Start ()
    {
		lighting = light.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        tone += 0.002f;
		Camera.main.fieldOfView -= 0.01f;
        lerpedColor = Color.Lerp(Color.white, Color.red, tone);
        lighting.color = lerpedColor;

        if(lerpedColor == Color.red)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
