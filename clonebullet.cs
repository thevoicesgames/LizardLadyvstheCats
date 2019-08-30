using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clonebullet : MonoBehaviour {
    Vector3 direction;
    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ground")
        {
            Destroy(gameObject);
        }
    }
        void Start () {
        // direction = Vector3.MoveTowards(gameObject.transform.position, playermovement.target,0.1f);
        Vector3 playerposition = playermovement.target;
        playerposition.y += 8;
        direction = playerposition - gameObject.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //gameObject.transform.Translate(direction/10);
        
        gameObject.transform.position = gameObject.transform.position + (direction/200);
	}
}
