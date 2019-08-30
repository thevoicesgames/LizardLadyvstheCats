using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class accelerationregion : MonoBehaviour {

    public float strength;

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<playermovement>().speed = gameObject.transform.up * strength;
            other.gameObject.GetComponent<playermovement>().accelerationregion();
        }


    }
        // Use this for initialization
        void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
