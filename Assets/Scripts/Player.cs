using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float speedH = 6.0f;
    public int idols;
    private float yaw = 0.0f;
    bool isLit;
    bool leverState;
    bool bigFireLit;
    private int frameCount;
    bool finalAudio;

    public GameObject torchLight;
    public GameObject smoke;
    public GameObject torch;
    public GameObject note1;
    public GameObject flame;
    public GameObject trigger;
    public GameObject door;
    public GameObject windLever1;
    public GameObject windLever2;
    public GameObject bigFlame;
    public Camera cam;
    public GameObject noteCanvas;
    public GameObject lever;
    public GameObject lastIdol;
    GameObject bucket;
    GameObject fire;
    public GameObject wind;

    public GameObject moustache;
    public GameObject moustache2;
    public GameObject moustache3;
    public GameObject moustache4;
    public GameObject moustache5;

    public Text score;

    public AudioClip burnSound;
    public AudioClip victory;

    Rigidbody rb;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        isLit = false;
        torchLight.SetActive(false);
        idols = 0;
        leverState = false;
        windLever1.SetActive(true);
        bigFlame.SetActive(false);
        door.SetActive(false);
        lastIdol.SetActive(false);

        moustache.SetActive(false);
        moustache2.SetActive(false);
        moustache3.SetActive(false);
        moustache4.SetActive(false);
        moustache5.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.timeScale == 1.0f)
        {
            bigFlame.transform.LookAt(transform.position);
            PlayerMove();
            TorchLit();

            if (Input.GetKey("joystick button 2"))
            {
                float distance = Vector3.Distance(transform.position, note1.transform.position);
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.transform.gameObject == note1 && distance <= 1)
                {
                    if (noteCanvas.GetComponent<Canvas>().enabled == false)
                    {
                        noteCanvas.GetComponent<Canvas>().enabled = true;
                    }
                }
            }
            else
            {
                noteCanvas.GetComponent<Canvas>().enabled = false;
            }

            if(Input.GetKeyDown("joystick button 0"))
            {
                float distance = Vector3.Distance(transform.position, lever.transform.position);
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "lever")
                {
                    if (leverState)
                    {
                        windLever1.SetActive(true);
                        windLever2.SetActive(false);
                        lever.transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        windLever1.SetActive(false);
                        windLever2.SetActive(true);
                        lever.transform.Rotate(0, 0, -90);
                    }

                    leverState = !leverState;
                }
            }
        }

        if(idols == 1)
        {
            moustache.SetActive(true);
        }
        if(idols == 2)
        {
            moustache2.SetActive(true);
            Destroy(moustache);
        }
        if(idols == 3)
        {
            moustache4.SetActive(true);
            lastIdol.SetActive(true);
            Destroy(moustache3);
        }

        if(idols == 4)
        {
            Destroy(moustache4);
            moustache5.SetActive(true);
            if(finalAudio == false)
            {
                AudioSource.PlayClipAtPoint(victory, moustache5.transform.position, 1f);
                finalAudio = true;
            }
        }
    }

    void PlayerMove()
    {
        float posx = Input.GetAxis("Left Stick X Axis");
        float posy = Input.GetAxis("Left Stick Y Axis");
        transform.Translate(posx / 50, 0, 0);
        transform.Translate(0, 0, -posy / 50);

        yaw += speedH * Input.GetAxis("Right Stick X Axis");

        transform.eulerAngles = new Vector3(0, yaw, 0.0f);

        if (!Input.anyKeyDown)
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
    }

    void TorchLit()
    {
        flame.SetActive(isLit);
        if (isLit)
        {
            frameCount++;

            if (frameCount == 30)
            {
                GameObject clone;
                clone = Instantiate(smoke, torch.transform.position + new Vector3(0, 0.3f, 0), torch.transform.rotation);
                clone.transform.localScale = Vector3.one * 0.02f;
                clone.gameObject.tag = "smoke";
                Smoke smokeScript = clone.GetComponent<Smoke>();
                smokeScript.player = this.gameObject.transform;
                frameCount = 0;
            }
            if (Input.GetKey("joystick button 1"))
            {
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "destroyable")
                {
                    GameObject target = hit.transform.gameObject;
                    float distance = Vector3.Distance(transform.position, target.transform.position);
                    if (distance <= 1)
                    {
                        AudioSource.PlayClipAtPoint(burnSound, target.transform.position, 0.15f);
                        Destroy(target);
                    }
                }
                if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "idol")
                {
                    GameObject target = hit.transform.gameObject;
                    float distance = Vector3.Distance(transform.position, target.transform.position);
                    if (distance <= 2)
                    {
                        AudioSource.PlayClipAtPoint(burnSound, target.transform.position, 0.15f);
                        Destroy(target);
                        idols += 1;
                        score.text = idols.ToString();
                    }
                }
            }
        }

        if (Input.GetKeyDown("joystick button 0"))
        {
            
            
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;

            if (isLit && Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "bucket")
            {
                bucket = hit.transform.gameObject;
                float distance = Vector3.Distance(transform.position, bucket.transform.position);
                if (distance <= 0.6)
                {
                    torchLight.SetActive(!isLit);
                    isLit = !isLit;
                }
            }

            if (isLit && Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "burn")
            {
                bucket = hit.transform.gameObject;
                float distance = Vector3.Distance(transform.position, bucket.transform.position);
                if (distance <= 1.2)
                {
                    AudioSource.PlayClipAtPoint(burnSound, bucket.transform.position, 0.15f);
                    bigFlame.SetActive(true);
                    bigFireLit = true;
                }
            }

            if (isLit == false && Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "fire")
            {
                fire = hit.transform.gameObject;
                float distance2 = Vector3.Distance(transform.position, fire.transform.position);
                if (distance2 <= 0.6)
                {
                    AudioSource.PlayClipAtPoint(burnSound, torch.transform.position, 0.15f);
                    torchLight.SetActive(!isLit);
                    isLit = !isLit;
                }
            }

            if (isLit == false && Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "burn" && bigFireLit)
            {
                fire = hit.transform.gameObject;
                float distance2 = Vector3.Distance(transform.position, fire.transform.position);
                if (distance2 <= 1.2)
                {
                    AudioSource.PlayClipAtPoint(burnSound, torch.transform.position, 0.15f);
                    torchLight.SetActive(!isLit);
                    isLit = !isLit;
                }
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<GameObject>() == wind)
        {
            isLit = false;
            torchLight.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "moustache")
        {
            SceneManager.LoadScene("GameOver");
        }

        if(collision.gameObject.tag == "final")
        {
            SceneManager.LoadScene("MainMenu");
        }

        if(collision.gameObject.tag == "trigger")
        {
            door.SetActive(true);
            Destroy(collision.gameObject);
            Destroy(moustache2);
            moustache3.SetActive(true);
        }
    }
}
