using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimasistcapsule : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.transform.up = Vector3.up;
	}
}
