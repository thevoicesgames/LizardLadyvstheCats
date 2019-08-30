using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deparent : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = null;
           
         

            //st.text = "yes";


        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
