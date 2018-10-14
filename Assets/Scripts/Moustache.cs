using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Moustache : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;
    GameObject closest;
    float posy;
	// Use this for initialization
	void Start ()
    {
        posy = transform.position.y;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        closest = FindClosestByTag("smoke");

        if(distance <= 4)
        {
            agent.SetDestination(player.transform.position);
        }

        else if(closest != null)
        {
            agent.SetDestination(closest.transform.position);
        }

        transform.position = new Vector3(transform.position.x, posy, transform.position.z);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    GameObject FindClosestByTag(string tag)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
