using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autocameracontrol : MonoBehaviour {

    Quaternion rot;
    void OnTriggerEnter(Collider other)
    {

        rot = other.gameObject.transform.localRotation;
        
    }
            // Use this for initialization
            void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rot, 0.05f);
	}
}
