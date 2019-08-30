using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falloutoflevel : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<playermovement>().takedamage(10);
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
