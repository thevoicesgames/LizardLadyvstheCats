using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubegravity : MonoBehaviour {
    public GameObject levelobject;
    public float gravity;
    public bool fixedplane;
    // Use this for initialization
    void Start () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "baddie")
        {
            other.gameObject.GetComponent<baddie>().cube = true;
            other.gameObject.GetComponent<baddie>().top = gameObject.transform.up;
            other.gameObject.GetComponent<baddie>().gravity = gravity;
        }
        if (other.gameObject.tag == "Player")
        {
            if(!levelobject.GetComponent<rotatelevel>().inspheregravity)
            {
               levelobject.GetComponent<rotatelevel>().sphere = false;
            }
            levelobject.GetComponent<rotatelevel>().cubegravityregion = gameObject;
            levelobject.GetComponent<rotatelevel>().cube = true;
            if(fixedplane)
            levelobject.GetComponent<rotatelevel>().fixedplane = true;
            levelobject.GetComponent<rotatelevel>().incubegravity = true;
            levelobject.GetComponent<rotatelevel>().player.GetComponent<playermovement>().gravity = gravity;

        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "baddie")
        {
            other.gameObject.GetComponent<baddie>().cube = true;
            other.gameObject.GetComponent<baddie>().top =  gameObject.transform.up; 
        }
    }

        void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            if (levelobject.GetComponent<rotatelevel>().cubegravityregion == gameObject)
            {
                levelobject.GetComponent<rotatelevel>().incubegravity = false;
                levelobject.GetComponent<rotatelevel>().fixedplane = false;
                if (levelobject.GetComponent<rotatelevel>().inspheregravity == false)
                    levelobject.GetComponent<rotatelevel>().sphere = false;
                if (gameObject && levelobject.GetComponent<rotatelevel>().sphere == true)
                levelobject.GetComponent<rotatelevel>().cube = false;
            }
        }
        if (other.gameObject.tag == "baddie")
        {
            other.gameObject.GetComponent<baddie>().cube = false;
        }
        }
        // Update is called once per frame
        void Update () {
		
	}
}
