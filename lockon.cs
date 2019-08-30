using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockon : MonoBehaviour {
    public static GameObject locktarget;
    public GameObject camera;
    void OnTriggerStay(Collider other)
    {
       // if (cameracontrol.locked)
            if (other.gameObject.tag == "baddie")
            {
            if(!other.gameObject.GetComponent<baddie>().dead)
                if (locktarget != null)
                {
                    if(!cameracontrol.locked)
                    if (Vector3.Distance(camera.transform.position, other.gameObject.transform.position) < Vector3.Distance(camera.transform.position, locktarget.transform.position))
                    {
                        locktarget = other.gameObject;
                    }
                }
                else
                    locktarget = other.gameObject;
            }
        
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
