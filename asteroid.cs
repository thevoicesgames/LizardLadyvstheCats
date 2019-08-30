using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour {
    public GameObject gravity;
    public GameObject levelrot;
    public GameObject smallasteroid;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="playerbullet" || other.tag == "enemybullet")
        {
            Destroy(other.gameObject);
            GameObject gam = Instantiate(smallasteroid,gameObject.transform);
            
            gam.transform.parent = levelrot.transform;
            gam.transform.position = gameObject.transform.position;
            gam.GetComponent<asteroid>().gravity = gravity;
            gam.GetComponent<asteroid>().levelrot = levelrot;
            gam.GetComponent<asteroid>().smallasteroid = smallasteroid;
            gam.transform.localScale = gameObject.transform.localScale *0.8f;
          
            gam.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
          
            GameObject gam2 = Instantiate(smallasteroid, gameObject.transform);
            gam2.transform.parent = levelrot.transform;
            gam2.transform.position = gameObject.transform.position;
            gam2.transform.Rotate(gameObject.transform.forward,180);
            gam2.GetComponent<asteroid>().gravity = gravity;
            gam2.GetComponent<asteroid>().levelrot = levelrot;
            gam2.GetComponent<asteroid>().smallasteroid = smallasteroid;
            gam2.transform.localScale = gameObject.transform.localScale * 0.8f;
            gam2.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;

      

            Destroy(gameObject);
           
        }
    }

        // Use this for initialization
        void Start () {
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity+ gameObject.transform.forward*20;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.GetComponent<Rigidbody>().AddForce((gravity.transform.position- gameObject.transform.position) /50);
	}
}
