﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROTATE : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.transform.Rotate(0f, 0.5f, 0f);
	}
}
