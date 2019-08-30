using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reardetector : MonoBehaviour {
    public quadroopeed master;
    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {

            master.attack(other.gameObject.transform.position);
        }
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
