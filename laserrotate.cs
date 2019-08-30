using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserrotate : MonoBehaviour {
    public float rotatespeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.transform.RotateAround(gameObject.transform.position, gameObject.transform.right, rotatespeed);
	}
}
