using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag!="aimbox" && other.gameObject.tag != "baddie" && other.gameObject.tag != "playerbullet" && other.gameObject.tag != "Player")
                Destroy(gameObject);
        
    }
        // Update is called once per frame
        void FixedUpdate () {
        gameObject.transform.Translate(0, 0, 0.4f);
	}
}
