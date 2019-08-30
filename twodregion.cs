using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twodregion : MonoBehaviour {
    public GameObject aimcamera;
    public bool twinstickaiming;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<playermovement>().state = 4;
            other.gameObject.GetComponent<playermovement>().backwards = false;
            other.gameObject.transform.forward = new Vector3(other.gameObject.GetComponent<playermovement>().speed.x, 0, 0);
            if(!twinstickaiming)
            aimcamera.GetComponent<cameracontrol>().twodregion = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<playermovement>().state = 2;
            other.gameObject.GetComponent<playermovement>().walljump = false;
            aimcamera.GetComponent<cameracontrol>().twodregion = false;
            aimcamera.GetComponent<cameracontrol>().secondtarget.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
