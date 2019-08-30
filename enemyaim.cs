using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyaim : MonoBehaviour {
    
    public GameObject parent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.transform.forward = parent.GetComponent<enemyik>().lookObj.transform.position - gameObject.transform.position;
      
	}
}
