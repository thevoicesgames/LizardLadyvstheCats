using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inthelight : MonoBehaviour {
    public UnityEngine.UI.Text tex;
    public GameObject master;
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            master.gameObject.GetComponent<quadroopeed>().checkcollisionupper(other.transform.position);
         
        }
   
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            master.gameObject.GetComponent<quadroopeed>().upperlit=false;
          
        }
     

    }
        // Use this for initialization
        void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
